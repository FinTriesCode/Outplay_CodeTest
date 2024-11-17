enum JewelKind
{
    Empty,
    Red,
    Orange,
    Yellow,
    Green,
    Blue,
    Indigo,
    Violet
}

enum MoveDirection
{
    Up,
    Down,
    Left,
    Right
}

struct Move
{
    public int x;
    public int y;
    public MoveDirection direction;
}

int GetWidth();
int GetHeight();

JewelKind GetJewel(int x, int y);
void SetJewel(int x, int y, JewelKind kind);

int CalcWeighings(int x, int y)
{
    int _bestWeight = 0;
    int _rollingWeightHori = 0;
    int _rollingWeightVert = 0;

    //create a ref to the current gem and include this gem in the weighing.
    JewelKind _currentJewel = GetJewel(x, y);
    _rollingWeightHori++;
    _rollingWeightVert++;


    //check right
    for(int i = x + 1; i < GetWidth() - 1; i++)
    {
        JewelKind _tempJewel = GetJewel(i, y);

        if(_currentJewel != _tempJewel) break;
        else
        {
            _rollingWeightHori++;
        }
    }
    
    //check left
    for(int i = x - 1; i >= 0; i--)
    {
        JewelKind _tempJewel = GetJewel(i, y);

        if(_currentJewel != _tempJewel) break;
        else
        {
            _rollingWeightHori++;
        }
    }

    //check up
    for(int i = y + 1; i < GetHeight() - 1; i++)
    {
        JewelKind _tempJewel = GetJewel(x, i);

        if(_currentJewel != _tempJewel) break;
        else
        {
            _rollingWeightVert++;
        }
    }
    
        //check down
    for(int i = y - 1; i >= 0; i--)
    {
        JewelKind _tempJewel = GetJewel(x, i);

        if(_currentJewel != _tempJewel) break;
        else
        {
            _rollingWeightVert++;
        }
    }


    int _combinedWeightReturned = 0;

    if(_rollingWeightHori >= 3)
        _combinedWeightReturned += _rollingWeightHori;
    if(_rollingWeightVert >= 3)
        _combinedWeightReturned += _rollingWeightVert;

    return _combinedWeightReturned;
}

Move CalculateBestMoveForBoard()
{
    Move _move = new Move();
    int _largestWeight = 0;

    for(int x = 0; x < GetWidth() - 1; x++)
    {
        for(int y = 0; y < GetWidth() - 1; y++)
        {
            //switch (x, y) and (X + 1, y) jewels locations
            JewelKind _tempJewel = GetJewel(x, y);
            SetJewel(x, y, GetJewel(x + 1, y));
            SetJewel(x + 1, y, _tempJewel);

            _calcedWeight = CalcWeighings(x, y) + CalcWeighings(x + 1, y);

            if(_largestWeight < _calcedWeight)
            {
                _largestWeight = _calcedWeight;
                _move.x = X;
                _move.y = Y;
                _move.direction = Right;
            }

            //swap back to origin pos to continue for loop's iteration
            _tempJewel = GetJewel(x, y);
            SetJewel(x, y, GetJewel(x + 1, y));
            SetJewel(x + 1, y, _tempJewel)l
        }
    }

    for(int x = 0; x < GetWidth() - 1; x++)
    {
        for(int y = 0; y < GetWidth() - 1; y++)
        {
            //switch (x, y) and (X + 1, y) jewels locations
            JewelKind _tempJewel = GetJewel(x, y);
            SetJewel(x, y, GetJewel(x, y + 1));
            SetJewel(x, y + 1, _tempJewel);

            _calcedWeight = CalcWeighings(x, y) + CalcWeighings(x, y + 1);

            if(_largestWeight < _calcedWeight)
            {
                _largestWeight = _calcedWeight;
                _move.x = X;
                _move.y = Y;
                _move.direction = Down;
            }

            //swap back to origin pos to continue for loop's iteration
            _tempJewel = GetJewel(x, y);
            SetJewel(x, y, GetJewel(x, y + 1));
            SetJewel(x, y + 1, _tempJewel)l
        }
    }
    return _largestWeight;
}

