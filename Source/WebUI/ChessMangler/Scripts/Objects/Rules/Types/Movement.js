/// <reference path="../../JQuery/jquery-1.7-vsdoc.js" />

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

var _thisObject;

//noinspection JSUnusedAssignment
var Movement = Class.extend({
    init: function () {
        //alert("hello init M");
        _thisObject = this;
        this.PieceID = '';
    },
    test: function () {
        alert("hello Movement");
    },
    RunAllRules: function () {

    },
    Is_One_of_my_pieces: function (pieceID) {
        //Checks to see if piece passed in is player's own

        //get player of current piece
        window._draggedPiecePlayer = window._rules.GetPlayerByName(window._draggedPieceID);

        //Get player of what we are hovering over
        window._hoveringOverPlayer = window._rules.GetPlayerByName(pieceID);

        var isMyPiece = '';
        if (window._draggedPiecePlayer == window._hoveringOverPlayer) {
            isMyPiece = true;

            //for "easy mode"..
            //Color Square red
            //Set CSS to Red (remember to set back to it was on dragend!
        } else {
            isMyPiece = false;
        }

        return isMyPiece;
    },
    Out_of_Bounds: function (spot) {
        return true;
    },
    Blocked: function (fromAddress, toAddress) {
        //expecting type to be "orthogonal" or "diagonal"
        //expecting spots to be an array of spots

        //[spot1, spot2, etc)

        //if orthogonal or diagonal route is not blocked
        return true;
    },
    Run: function (movementRule) {
        switch (movementRule) {
            case "Not on own Piece":
                return _thisObject.Is_One_of_my_pieces(this.PieceID);
                break;
            case "Some other Rule":
                return false;
                break;
            default:
                return false;
        }
    }
});

//    Blocked2: function (type, spots) {
//        //expecting type to be "orthogonal" or "diagonal"
//        //expecting spots to be an array of spots
//        
//        //[spot1, spot2, etc)

//        //if orthogonal or diagonal route is not blocked
//        return true;
//    },