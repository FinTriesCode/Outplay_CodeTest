using System;

class Program
{

    bool TryCalculateXPositionAtHeight(float h, Vector2 p, Vector2 v, float G, float w, ref float xPosition)
    {
        //init data
        float _time, _remainingDist = 0;
        int _numOfHorizontanBounces = 0;

        float _distanceBeforeBounceX;
        int _sumOfBounces;

        float root1, root2, _discriminent;


        //init quadratic equasion data (ax^2 + bx + c = 0) where x is t
        float a, b, c;

        a = G / 2f;
        b = -v.y;
        c = p.y - h;

        _discriminent = b * b - 4f * a * c; //calc discriminent (b^2 - 4ac)


        //check for valid discriminent 
        //if less than 0, height (h) is never reached
        if (_discriminent < 0)
            return false;


        //calculate routes (+ and -) of quadratic equasion -> (-b +- sqrt->b^2-4ac / 2a)
        root1 = (-b + Mathf.Sqrt(_discriminent)) / (2f * a);
        root2 = (-b - Mathf.Sqrt(_discriminent)) / (2f * a);


      //choose correct root based on initial vertacle velocity
      if (v.y >= 0)
            _time = root1;
        else
            _time = root2;


        //horizontal distance travelled before registering a bounce(s)
        _distanceBeforeBounceX = v.x * _time + p.x;


        //calc sum of bounce(s) -> using width (w) to find boundaries
        _sumOfBounces = (int)(_distanceBeforeBounceX / w);

        //calc remaining horizontal distance after previous full bounce
        _remainingDist = _distanceBeforeBounceX - _sumOfBounces * w;

        //check and adjust (if needed) final x pos based on sum of _sumOfBounces
        //xPosition not defined as is an ref-type parameter
        if (_numOfHorizontanBounces % 2 == 1)
            xPosition = w - _remainingDist; //odd num of bounces -> reflect based on this num
        else
            xPosition = _remainingDist; //else even num, keep remaining distance


        return true;

    }
}