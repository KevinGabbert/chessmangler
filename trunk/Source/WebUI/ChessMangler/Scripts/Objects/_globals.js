//Copyright 2012, Kevin Gabbert

//   Licensed under the Apache License, Version 2.0 (the "License");
//   you may not use this file except in compliance with the License.
//   You may obtain a copy of the License at

//       http://www.apache.org/licenses/LICENSE-2.0

//   Unless required by applicable law or agreed to in writing, software
//   distributed under the License is distributed on an "AS IS" BASIS,
//   WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
//   See the License for the specific language governing permissions and
//   limitations under the License.

var _draggedPieceID; //cheat.  this data needs to be stored elsewhere

var _config = window._rulesDef.ChessConfig;

var _rules = new Rules();
var _conditions = new window.Conditions(); //grab rules for all pieces

var _newPiece = new Piece();
var _newSpot = new Spot();

var _debug = new Debug();

var _draggedPiecePlayer;
var _hoveringOverPlayer;
var _pieceHoveredOver;
var _lastStartingSpot;
var _lastDroppedSpot;

var _moveIsLegal = true;