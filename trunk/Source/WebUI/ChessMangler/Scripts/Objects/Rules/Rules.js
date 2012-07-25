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

//todo: this should be renamed GameInfo
//noinspection JSUnusedAssignment
var Rules = Class.extend({
    init: function () {
        this.Movement = new window.Movement();
        this.Enforce = new window.EnforceRule();
    },
    RunMovementRules: function (pieceToRunRulesOn) {
        //alert("Hello " + pieceToRunRulesOn);
    },
    GetPlayerByName: function (nameToFind) {

        var returnVal = '';

        var i;
        for (i = 0; i <= window._config.PieceDefs.length - 1; i++) {

            var player = window._config.PieceDefs[i].player;
            var name = window._config.PieceDefs[i].name;

            if (name == nameToFind) {
                returnVal = player;
                break;

            } else {
                returnVal = "none";
            }
        }

        return returnVal;
    },
    //This function 
    AmIHoveringOverMySpace: function (thisPieceID) {
        
        //looks at spot for piece. returns true

        
    },
    AmIHoveringOverMyPiece: function (thisPieceID) {

        //get player of current piece
        window._draggedPiecePlayer = window._rules.GetPlayerByName(window._draggedPieceID);

        //Get player of what we are hovering over
        window._hoveringOverPlayer = window._rules.GetPlayerByName(thisPieceID);

        //todo: this is temporary logic.  we need to accomodate for more than 2 players eventually.

        var isMyPiece = '';
        if (window._draggedPiecePlayer == window._hoveringOverPlayer) {

            isMyPiece = true; // "Yeah!
        }
        else if (window._hoveringOverPlayer == null) { //"none"
            isMyPiece = null; // "ummmmm...";
        } else {
            isMyPiece = false; //"No...";
        }

        //        // debug code - move elsewhere
        //        //-------------------------------------------------------------------------------------------------

        //        var debugIsMyPiece = '';
        //        if (window._moveIsLegal) {
        //            debugIsMyPiece = "Yeah!!";
        //        } else if (window._moveIsLegal == null) {
        //            debugIsMyPiece = "ummmmm....";
        //        } else {
        //            debugIsMyPiece = "No....";
        //        }

        //        window._debug.SPOTSaysPopulatePlayerHoverStuff(window._hoveringOverPlayer, window._draggedPiecePlayer, debugIsMyPiece);

        //        //-----------------------------------------------------------------------------------------------------

        return isMyPiece;
    },
    OkToSetPiece: function (thisPieceID, thisSpotID, spot) {

        var allPiecesCan = window._conditions; //alias for clarity. - rules for all pieces

        //---------------------------- debug ---------------------------------------------------------
        $("#AllPiecesCanMove").text(allPiecesCan.MovePiece());
        $("#AllPiecesCaptureOwn").text(allPiecesCan.CaptureOwnPiece().toString());
        $("#AllPiecesReplaceOpposing").text(allPiecesCan.ReplaceOpposingPiece().toString());
        $("#AllPiecesMoveOutOfBounds").text(allPiecesCan.OutOfBounds().toString());

        // ---------------------- thisPieceID  ------------------------
        //get rules from rules file  for thisPieceID
        //check special move rules here
        //  This really means
        //    en passant
        //      check to set flag for other player
        //        flag is set in rules file
        //     check to see hb b own eligibility for en passant
        //     check flag in rules file
        //       this flag is set as a node with a pawn column. 8 possible nodes.
        //    castle
        //    promotion
        //    If king, see if in check
        //}

        //All pieces can move by default. Set a condition "can't move" if piece needs to remain stationary
        //if "Can't move" node is found, then set this to false
        //var authorizedToMovePiece = true;      //true for now - true for chess.  won't be true for stars in Star Trek KG
        // ---------------------- thisPieceID  ------------------------

        //get info needed to implement rules that apply
        var hoveringOverMyPiece = window._rules.AmIHoveringOverMyPiece(thisPieceID);  //is dragged piece hovering over our dragged piece's team piece?
        window._debug.ShowHoverInfo(hoveringOverMyPiece, thisPieceID, thisSpotID);
        
        //var hoveringOverMySpace = window._rules.AmIHoveringOverMyOwnSpace(thisPieceID);
        
        
        //if hovering over my own space, then allow drop.  rules that come later can deny drop.
        
        var okToSetPiece = false;
        if (allPiecesCan.MovePiece()) {

            //var pieceNotSetReason = ""; //display in debug window why we cant set piece

            if (allPiecesCan.ReplaceOpposingPiece() && (!hoveringOverMyPiece)) { //This means that we are hovering over an opposing Piece
                okToSetPiece = true; //add on other rule bools as needed 
            } else if (!hoveringOverMyPiece) {
                //No need to check for rules here.  yet.
                okToSetPiece = true;
            }

            if ((!allPiecesCan.CaptureOwnPiece()) && hoveringOverMyPiece) {
                okToSetPiece = false;
            }
        }

        //debug code
        //------------------------------------

        //update "Authorized" boxes in debug

        //Now that we know what we have, we can run a set of rules.
        //Can this piece take this other piece?
        //PieceHoveredOver will turn bold or red or something to show that it can be taken

        var debugMode = true;
        if (debugMode) {
            if (okToSetPiece == null) {
                //don't color any squares

            } else if (okToSetPiece) {

                //color square Green
                $(spot).addClass("ui-state-highlight");

                //spot.AllowDrop();
            }
            else if (!okToSetPiece) {
                $(spot).addClass("ui-state-highlight2"); //color square Red
                //spot.DenyDrop(); 
            }
        }


        //squares are reset in spot.Out

        //------------------------------------
        //        //run 'drop' rules here

        //        //for example, if you can't drop on same player..

        //        //                window._rules.RunMovementRules(_thisPiece);
        //        //                window._rules.Movement.test();

        //        //                var x = window._rules.Movement.Is_One_of_my_pieces();

        //        //if move was made illegal during this event
        //        if (!_moveIsLegal) {
        //            $(thisPieceID).removeClass("legalMove");
        //        }

        return okToSetPiece;
    }
});

//look up piece.player from rules (this wil go in rules object)

//                var xx = $.grep(window._rulesDef, function (n) {
//                    return n == "PieceDefs";
//                });
