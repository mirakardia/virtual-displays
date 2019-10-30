using UnityEngine;
using System.Collections.Generic;
using Kinect = Windows.Kinect;

public class BodySourceView : MonoBehaviour 
{
    public GameLandingEffect publicDisplay;

    public Material BoneMaterial;
    public GameObject BodySourceManager;
    public GameObject JOINT;
    public GameObject inactiveJoint;
    public GameObject Line;

    static float skeletonScaling = 4f;
    public float horizontalScale = 2.0f;
    float currentHorizontalOffset = 0f;

    public delegate void BodyEventHandler(ulong id);
    public static event BodyEventHandler OnPlayerEntered;
    public static event BodyEventHandler OnPlayerExited;

    private Dictionary<ulong, GameObject> _Bodies = new Dictionary<ulong, GameObject>();
    private BodySourceManager _BodyManager;
    private Dictionary<Kinect.JointType, Kinect.JointType> _BoneMap = new Dictionary<Kinect.JointType, Kinect.JointType>()
    {
        { Kinect.JointType.FootLeft, Kinect.JointType.AnkleLeft },
        { Kinect.JointType.AnkleLeft, Kinect.JointType.KneeLeft },
        { Kinect.JointType.KneeLeft, Kinect.JointType.HipLeft },
        { Kinect.JointType.HipLeft, Kinect.JointType.SpineBase },
        
        { Kinect.JointType.FootRight, Kinect.JointType.AnkleRight },
        { Kinect.JointType.AnkleRight, Kinect.JointType.KneeRight },
        { Kinect.JointType.KneeRight, Kinect.JointType.HipRight },
        { Kinect.JointType.HipRight, Kinect.JointType.SpineBase },
        
        //{ Kinect.JointType.HandTipLeft, Kinect.JointType.HandLeft },
        //{ Kinect.JointType.ThumbLeft, Kinect.JointType.HandLeft },
        { Kinect.JointType.HandLeft, Kinect.JointType.WristLeft },
        { Kinect.JointType.WristLeft, Kinect.JointType.ElbowLeft },
        { Kinect.JointType.ElbowLeft, Kinect.JointType.ShoulderLeft },
        { Kinect.JointType.ShoulderLeft, Kinect.JointType.SpineShoulder },
        
        //{ Kinect.JointType.HandTipRight, Kinect.JointType.HandRight },
        //{ Kinect.JointType.ThumbRight, Kinect.JointType.HandRight },
        { Kinect.JointType.HandRight, Kinect.JointType.WristRight },
        { Kinect.JointType.WristRight, Kinect.JointType.ElbowRight },
        { Kinect.JointType.ElbowRight, Kinect.JointType.ShoulderRight },
        { Kinect.JointType.ShoulderRight, Kinect.JointType.SpineShoulder },
        
        { Kinect.JointType.SpineBase, Kinect.JointType.SpineMid },
        { Kinect.JointType.SpineMid, Kinect.JointType.SpineShoulder },
        { Kinect.JointType.SpineShoulder, Kinect.JointType.Neck },
        { Kinect.JointType.Neck, Kinect.JointType.Head },
    };
    
    void Update () 
    {
        if (BodySourceManager == null)
        {
            return;
        }
        
        _BodyManager = BodySourceManager.GetComponent<BodySourceManager>();
        if (_BodyManager == null)
        {
            return;
        }
        
        Kinect.Body[] data = _BodyManager.GetData();
        if (data == null)
        {
            return;
        }
        
        List<ulong> trackedIds = new List<ulong>();
        foreach(var body in data)
        {
            if (body == null)
            {
                continue;
              }
                
            if(body.IsTracked)
            {
                trackedIds.Add (body.TrackingId);
            }
        }
        
        List<ulong> knownIds = new List<ulong>(_Bodies.Keys);
        
        // First delete untracked bodies
        foreach(ulong trackingId in knownIds)
        {
            if(!trackedIds.Contains(trackingId))
            {
                if (OnPlayerExited != null)
                {
                    OnPlayerExited(trackingId);
                }
                Destroy(_Bodies[trackingId]);
                _Bodies.Remove(trackingId);
            }
        }

        foreach(var body in data)
        {
            if (body == null)
            {
                continue;
            }
            
            if(body.IsTracked)
            {
                if(!_Bodies.ContainsKey(body.TrackingId))
                {
                    _Bodies[body.TrackingId] = CreateBodyObject(body.TrackingId);
                    if (OnPlayerEntered != null)
                    {
                        OnPlayerEntered(body.TrackingId);
                    }
                }

                RefreshBodyObject(body, _Bodies[body.TrackingId]);
            }
        }
    }
    private GameObject CreateBodyObject(ulong id)
    {
        GameObject body = new GameObject("Body:" + id);
        body.tag = "Player";
        body.transform.position += Vector3.up * 0.5f;

        for (Kinect.JointType jt = Kinect.JointType.SpineBase; jt <= Kinect.JointType.ThumbRight; jt++)
        {
            GameObject jointObj;
            if (jt.ToString().Equals("HandRight") || jt.ToString().Equals("HandLeft") || jt.ToString().Equals("Head"))
            {
                jointObj = Instantiate(JOINT);
                jointObj.tag = "Player";
                jointObj.transform.localScale = new Vector3(0.15f, 0.15f, 0.000001f);
            }
            else
            {
                jointObj = Instantiate(inactiveJoint);
                jointObj.transform.localScale = new Vector3(0.05f, 0.05f, 0.000001f);
            }
            
            GameObject Bone = Instantiate(Line);
            LineRenderer lr = Bone.GetComponent<LineRenderer>();
            lr.SetVertexCount(2);
            lr.material = BoneMaterial;
            lr.SetWidth(0.05f, 0.05f);

            jointObj.name = jt.ToString();

            jointObj.transform.parent = body.transform;
            Bone.transform.parent = body.transform;
            Bone.name= jt.ToString()+"B";
        }

        body.AddComponent<KinectBody>().set(horizontalScale);

        return body;
    }

    private void RefreshBodyObject(Kinect.Body body, GameObject bodyObject)
    {
        Kinect.Joint midPoint;
        body.Joints.TryGetValue(Kinect.JointType.SpineBase, out midPoint);
        if (midPoint != null) {
            bodyObject.GetComponent<KinectBody>().updateHorizontalOffset(midPoint.Position.X);
        }

        string kinectDataString = body.TrackingId + "|";

        for (Kinect.JointType jt = Kinect.JointType.SpineBase; jt <= Kinect.JointType.ThumbRight; jt++)
        {
            Kinect.Joint sourceJoint = body.Joints[jt];
            Kinect.Joint? targetJoint = null;

            Transform jointObj = bodyObject.transform.Find(jt.ToString());

            if (_BoneMap.ContainsKey(jt))
            {
                targetJoint = body.Joints[_BoneMap[jt]];
            }
            else if (jointObj.name.Equals("Head"))
            {

            }
            else
            {
                jointObj.gameObject.SetActive(false);
                continue;
            }

            jointObj.localPosition = GetVector3FromJoint(sourceJoint);

            kinectDataString += sourceJoint.JointType.ToString() + ":" + sourceJoint.Position.X + "," + sourceJoint.Position.Y + "," + sourceJoint.Position.Z + "," + ";";

            Transform Bone= bodyObject.transform.Find(jt.ToString()+"B");
            Bone.position = bodyObject.transform.position;

            if (jointObj.position.x < publicDisplay.leftEdge || jointObj.position.y > publicDisplay.topEdge ||
                jointObj.position.y < publicDisplay.bottomEdge || jointObj.position.x > publicDisplay.rightEdge)
            {
                jointObj.gameObject.SetActive(false);
                Bone.GetComponent<LineRenderer>().enabled = false;
                Bone.gameObject.SetActive(false);
            }
            else {
                jointObj.gameObject.SetActive(true);
                Bone.GetComponent<LineRenderer>().enabled = true;
                Bone.gameObject.SetActive(true);
            }

            if (!(jointObj.name.Equals("HandRight") || jointObj.name.Equals("HandLeft") || jointObj.name.Equals("Head")))
            {
                //jointObj.gameObject.SetActive(false);
            }

            if (targetJoint.HasValue)
            {
                Bone.GetComponent<LineRenderer>().SetPosition(0, GetVector3FromJoint(sourceJoint));
                Bone.GetComponent<LineRenderer>().SetPosition(1, GetVector3FromJoint(targetJoint.Value));
                Bone.GetComponent<LineRenderer>().SetColors(GetColorForState(sourceJoint.TrackingState), GetColorForState(targetJoint.Value.TrackingState));
            }
            else
            {
                Bone.GetComponent<LineRenderer>().enabled = false;
            }
        }

        // Log the final data string.
        Logger.Log("KINECT", "DATA", kinectDataString);
    }

    private static Color GetColorForState(Kinect.TrackingState state)
    {
        switch (state)
        {
        case Kinect.TrackingState.Tracked:
            return Color.green;

        case Kinect.TrackingState.Inferred:
            return Color.red;

        default:
            return Color.black;
        }
    }
    
    private static Vector3 GetVector3FromJoint(Kinect.Joint joint)
    {
        //Considering the vanshing point to be at (0,0,0)
        float x = joint.Position.X *10;
        float y = joint.Position.Y *10;
        float z = joint.Position.Z *10;
        Vector3 Projected = OnePointPrespectiveProjection(new Vector3(x, y, z), 5.5f);
        Vector3 Translated = new Vector3(Projected.x , Projected.y +7, Projected.z);
        
        return Translated;
    }
    public static Vector3 OnePointPrespectiveProjection(Vector3 coordinates,float d) {
        
        return new Vector3(coordinates.x /(skeletonScaling * (coordinates.z/d)), coordinates.y / (skeletonScaling * (coordinates.z/d)), d);
    }
}
