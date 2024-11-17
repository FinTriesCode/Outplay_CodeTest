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
    //weight variables 
        //weighing is used to find best match.
    int _bestWeight = 0;
    int _rollingWeightHori = 0;
    int _rollingWeightVert = 0;

    //create a ref to the current gem and include this gem in the weighing.
    JewelKind _currentJewel = GetJewel(x, y);
    _rollingWeightHori++;
    _rollingWeightVert++;


    //check right
    for(int i = x + 1; i < GetWidth() - 1; i++) //loop from current jewel pos to right of board (or until a non-match is found).
    {
        JewelKind _tempJewel = GetJewel(i, y); //temp jewel referenced at iterated position (i).

        if(_currentJewel != _tempJewel) break; //no match -> early termination.
        else
        {
            //confirmed match causes an incrementation of the weight.
            _rollingWeightHori++;
        }
    }
    
    //check left
        //same comments as right (with differences to directional iteration).
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
        //same comments as right (with differences to directional iteration and weighing is now vertical).
    for (int i = y + 1; i < GetHeight() - 1; i++)
    {
        JewelKind _tempJewel = GetJewel(x, i);

        if(_currentJewel != _tempJewel) break;
        else
        {
            _rollingWeightVert++;
        }
    }

    //check down
        //same comments as right (with differences to directional iteration and weighing is now vertical).
    for (int i = y - 1; i >= 0; i--)
    {
        JewelKind _tempJewel = GetJewel(x, i);

        if(_currentJewel != _tempJewel) break;
        else
        {
            _rollingWeightVert++;
        }
    }

    //weight checks
        //var to store possible combined (both directions) weighings.
    int _combinedWeightReturned = 0;

    //if the weight of a (all) direction(s) -> add to the combined (total) weight of the currently checked jewel.
    if(_rollingWeightHori >= 3)
        _combinedWeightReturned += _rollingWeightHori;
    if(_rollingWeightVert >= 3)
        _combinedWeightReturned += _rollingWeightVert;

    //return for use in calc best move function.
    return _combinedWeightReturned;
}

Move CalculateBestMoveForBoard()
{
    //init data
    Move _move = new Move(); //new move to be mutated throughout function
    int _largestWeight = 0; //used for calculating largest weight (to figure out optimal direction of movement)

    //loop through every jewel on the board
    for(int x = 0; x < GetWidth() - 1; x++)
    {
        for(int y = 0; y < GetWidth() - 1; y++)
        {
            //switch (x, y) and (X + 1, y) jewel locations
            JewelKind _tempJewel = GetJewel(x, y);
            SetJewel(x, y, GetJewel(x + 1, y));
            SetJewel(x + 1, y, _tempJewel);

            //calculate the current weight of the jewels and their moves
            _calcedWeight = CalcWeighings(x, y) + CalcWeighings(x + 1, y);

            //compare the calculated weight and the largest weight
            if(_largestWeight < _calcedWeight)
            {
                //if the largest weight is overwritten by the calculated weight
                    //update the optimal jewel's weight, position and desired movement direction
                        //note that this is left by default and is only changed to Right if overwritten.
                _largestWeight = _calcedWeight;
                _move.x = X;
                _move.y = Y;
                _move.direction = MoveDirection.Right;
            }

            //swap back to origin pos to continue for loop's iteration
            _tempJewel = GetJewel(x, y);
            SetJewel(x, y, GetJewel(x + 1, y));
            SetJewel(x + 1, y, _tempJewel)l
        }
    }

    //same comments as the board loop above and it's contents
        //main differences regard the directions. The default move direction is set to Up -> and is set to Down if overwritten.
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
                _move.direction = MoveDirection.Down;
            }

            //swap back to origin pos to continue for loop's iteration
            _tempJewel = GetJewel(x, y);
            SetJewel(x, y, GetJewel(x, y + 1));
            SetJewel(x, y + 1, _tempJewel)l
        }
    }

    //return best move
    return _move;
}

