using System;
using UnityEngine;

public class MapManager : MonoBehaviour
{
    public float xMin, xMax;
    public float yMin, yMax;
     public   int spaceBetweenDots;

    public static MapManager _instance;
    [SerializeField] GameObject LineObject;
    LineRenderer LineComponent;
    EdgeCollider2D mapCollider;
    Vector2[] linePoints;
    RaycastHit2D[] _hitinfo;
    void Start()
    {
        _instance = this;
        mapCollider = LineObject.GetComponent<EdgeCollider2D>();
        LineComponent = LineObject.GetComponent<LineRenderer>();
        CreateMap();
    }


    void CreateMap()
    {
        LineComponent.positionCount = Mathf.RoundToInt((Mathf.Abs(xMin) + Mathf.Abs(xMax)) / 2) + 1;
        LineComponent.SetPosition(0, new Vector2(xMin, UnityEngine.Random.Range(yMin, yMax)));
        linePoints= new Vector2[LineComponent.positionCount];
        linePoints[0] = LineComponent.GetPosition(0);
        for (int i = 1; i < LineComponent.positionCount; i++)
        {
            LineComponent.SetPosition(i, GetRandomPoint(LineComponent.GetPosition(i - 1).x));
            linePoints[i]= LineComponent.GetPosition(i);
            if (LineComponent.GetPosition(i).x >= xMax)
            {
                break;
            }
        
      }
            mapCollider.points = linePoints;
  

    }
    Vector2 GetRandomPoint(float previousXPoint)
    {
        int newXPoint = Mathf.RoundToInt(previousXPoint) + spaceBetweenDots;

        int yPoint = Mathf.RoundToInt(UnityEngine.Random.Range(yMin, yMax));


        return new Vector2Int(newXPoint, yPoint);
    }
   public float GetAnglesOfTwoPoints(Vector2 point) {
        float x = point.x;
      
        for (int i = 0; i < linePoints.Length -1; i++)
        {
            if (linePoints[i].x < x && x < linePoints[i+1].x)
            {
               
                float f = (Mathf.Atan2(linePoints[i + 1].y- linePoints[i].y, linePoints[i + 1].x - linePoints[i].x) * Mathf.Rad2Deg) ;
                if (f < 1 && f > -1)
                {
                    f= 0;
                }

                return  f;
            }
        }

        return 0;
    }



}
