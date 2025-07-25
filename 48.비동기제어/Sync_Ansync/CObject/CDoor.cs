﻿using System.Drawing;

namespace Sync_Ansync
{
    class CDoor : CBase
    {
        public Rectangle _rtDoorSide; //테두리
        public Rectangle _rtDoor;
        public CDoor(string sName)
        {
            strName = sName;
            _Pen = new Pen(Color.WhiteSmoke, 3);
            _Brush = new SolidBrush(Color.WhiteSmoke);   

            _rtDoorSide = new Rectangle(10, 70, 20, 60);
            _rtDoor = new Rectangle(10, 70, 20, 60);
        }
        public Pen fPenInfo()
        {
            return _Pen;
        }

        public SolidBrush fBrushInfo()
        {
            return _Brush;
        }

        public void fMove(int iMove)
        {
            fSquareMove(iMove);
        }

        protected void fSquareMove(int iMove)
        {
            Point oPoint = _rtDoor.Location;
            oPoint.Y = oPoint.Y + iMove;
            _rtDoor.Location = oPoint;
        }
    }
}
