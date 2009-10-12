using System;
using System.Collections.Generic;

using System.Text;
using System.IO;
using System.Drawing;

using ChessMangler.Engine.Types.Rules;
using ChessMangler.Engine.Interfaces;

namespace ChessMangler.Engine.Types
{
    public class Piece: IPiece
    {
        #region Properties

        string _imageDirectory;
        public string ImageDirectory
        {
            get
            {
                return _imageDirectory;
            }
            set
            {
                _imageDirectory = value;
            }
        }

        /// <summary>
        /// Auto Map info when Def is assigned
        /// </summary>
        bool _autoMapOnDefSet;
        public bool AutoMapOnDefSet
        {
            get
            {
                return _autoMapOnDefSet;
            }
            set
            {
                _autoMapOnDefSet = value;
            }
        }

        protected PieceDef _pieceDef = new PieceDef();
        public PieceDef Definition
        {
            get
            {
                return _pieceDef;
            }
            set
            {
                _pieceDef = value;

                if (this.AutoMapOnDefSet)
                {
                    this.Map_Info(value, true);
                }
            }
        }

        protected int _row;
        public int Row
        {
            get
            {
                return _row;
            }
            set
            {
                _row = value;
            }
        }

        protected int _column;
        public int Column
        {
            get
            {
                return _column;
            }
            set
            {
                _column = value;
            }
        }

        protected string _name;
        public string Name
        {
            get
            {
                return _name;
            }
            set
            {
                this._name = value;
            }
        }

        protected string _boardLocation;
        public string BoardLocation
        {
            get
            {
                return _boardLocation;
            }
            set
            {
                this._boardLocation = value;
            }
        }

        protected int _player;
        public int Player
        {
            get
            {
                return this.Definition.Player;
            }
            set
            {
                this.Definition.Player = value;
            }
        }

        public Color Color
        {
            get
            {
                return this.Definition.Color;
            }
            set
            {
                this.Definition.Color = value;
            }
        }

        public bool Disabled
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        protected Image _image;
        public Image Image
        {
            get
            {
                return _image;
            }
            set
            {
                _image = value;
            }
        }

        protected Movement _movement;
        public Movement Movement
        {
            get
            {
                return _movement;
            }
            set
            {
                _movement = value;
            }
        }

        protected Capture _capture;
        public Capture Capture
        {
            get
            {
                return _capture;
            }
            set
            {
                _capture = value;
            }
        }

        #endregion

        public Piece()
        {
        }

        public Piece(bool autoMapOnDefSet, string ImageDirectory)
        {
            this.AutoMapOnDefSet = autoMapOnDefSet;
        }
        public Piece(PieceDef pieceDef, bool verifyImage)
        {
            this.AutoMapOnDefSet = true;

            this.Map_Info(pieceDef, verifyImage);
        }
        public Piece(PieceDef pieceDef, bool autoMapOnDefSet, string ImageDirectory, bool verifyImage)
        {
            this.Map_Info(pieceDef, verifyImage);
        }

        public void Map_Info(PieceDef value, bool verifyImage)
        {
            //Look up attributes in passed config file and assign (such as image, and movement rules, etc..)

            this.Name = value.Name;
            this.Color = value.Color;
            this.BoardLocation = value.BoardLocation;
            this.Player = value.Player;
            this.ImageDirectory = value.ImageDirectory;

            if (this.ImageDirectory == null)
            {
                throw new DirectoryNotFoundException("no image directory has been set up (null encountered)");
            }

            if (verifyImage)
            {
                if (File.Exists(this.ImageDirectory + value.ImageName))
                {
                    //Bitmap will say "Parameter not valid" if any part of the file name is messed up..
                    this.Image = new Bitmap(this.ImageDirectory + value.ImageName);
                }
                else
                {
                    throw new FileNotFoundException("unable to find Piece Image:  " + this.ImageDirectory + value.ImageName);
                }
            }

            //TODO: this.Disabled = value.Disabled
        }
    }
}
