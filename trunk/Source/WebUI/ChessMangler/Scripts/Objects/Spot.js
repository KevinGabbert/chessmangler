/// <reference path="Scripts/JQuery/jquery-1.7-vsdoc.js" />

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

//its hard to come up with names!  location.. position.. both confusing.
var _thisSpot;
var _okToAccept;

var _pieceHoveredOver;

//noinspection JSUnusedAssignment
var Spot = Class.extend({
    init: function () {
        _thisSpot = this; //allows us to ref this object inside other local functions
    },
    AddSpotEvents: function (dropSelector) {

        $(dropSelector).droppable({
            hoverClass: 'spotDropHover', //This will be good for css, putting red X in Spot if needed.
            over: function (event) {
                //This apparently does not map if to the right of a piece

                var $thisObj = $(this);
                var thisSpotID = $thisObj.attr('id'); //data

                window._debug.PopulateDroppableHoverStuff(thisSpotID);

                var _pieceHoveredOver = _thisSpot.GetPieceID($thisObj);

                window._moveIsLegal = window._rules.OkToSetPiece(_pieceHoveredOver, thisSpotID, $thisObj);
                $("#AllPiecesLegal").text(window._moveIsLegal.toString());

                if (!window._moveIsLegal) {
                    //$(this).removeClass("thing");
                    //$(this).removeClass("ui-droppable");
                    //alert("illegal");
                }

                //test
                if (window._moveIsLegal) {
                    $thisObj.addClass("legalMove");
                } else {
                    $(".legalMove").removeClass("legalMove");
                }

                if (_pieceHoveredOver == undefined) { //|| _pieceHoveredOver == window._draggedPieceID
                    _pieceHoveredOver = ''; //There's no piece here, FOOL

                    window._moveIsLegal = true; //window._rules.okToSetPiece(_pieceHoveredOver, thisSpotID, _thisSpot); //temporary, for testing purposes.  A rule should be looked up here to allow this //todo:

                } else {
                    $("#" + window._draggedPieceID).removeClass("legalMove");
                }

                if (window._draggedPieceID != undefined) {

                    if ((_pieceHoveredOver != '') &&
                        (_pieceHoveredOver != window._draggedPieceID)) {

                        //DraggedPieceID should not be undefined
                        //window._rules.RunMovementRules(_pieceHoveredOver);

                        //window._moveIsLegal = window._rules.OkToSetPiece(_pieceHoveredOver, thisSpotID, _thisSpot);
                    } else {
                        // window._debug.PopulateERRORInfo("spot.over- error determining dragged piece");
                        //alert("spot.over - error determining dragged piece");
                        //note the error somehow, like some sort of indicator somewhere to show that rules aren't being
                        //parsed.
                    }
                }

                //remove
                //-------------------------------------------//----------------------------------------------------
                //if move was made illegal during this event
                if (!window._moveIsLegal) {
                    //$(thisPieceID).removeClass("legalMove");
                    //revert
                }

                //-------------------------------------------//----------------------------------------------------
            },
            out: function () {

                var $thisObj = $(this);
                $thisObj.removeClass("ui-state-highlight"); //uncolor piece background
                $thisObj.removeClass("ui-state-highlight2");

                $(".legalMove").removeClass("legalMove");

                //_thisPiece.GetSpot($thisObj.attr('id')).removeClass('ui-state-highlight'); //I'd rather do this, but it ain't working

                $thisObj.addClass("thing");
                $thisObj.addClass("ui-droppable");
                _thisSpot.AllowDrop($thisObj);
                _okToAccept = true;
            },
            drop: function (event, ui) {

                window._debug.ClearErrorsDiv();

                var $thisObj = $(this);
                var thisSpotID = $thisObj.attr('id'); //data

                window._lastDroppedSpot = thisSpotID;

                $("#draggedPieceDroppedSpot").text(window._lastDroppedSpot); //debug

                $thisObj.removeClass("ui-state-highlight"); //uncolor piece background
                $thisObj.removeClass("ui-state-highlight2");
                $("#draggedPieceStartingSpot").text(window._lastStartingSpot + " *dropped*");

                $(".legalMove").removeClass("legalMove");

                if (!_moveIsLegal) {

                    //Remove piece from window.lastDroppedSpot
                    //Append it to window.startingspot
                    //_thisSpot.AppendOrReplacePiece(pieceID, _moveIsLegal)


                    //event.preventDefault();
                }

                //run appropriate rules HERE
                //look them up HERE

                //CANCEL this event if rules for this piece move are not satisfied.

                //------------------------------------------------------------------
                //This still needs to be clarified
                //------------------------------------------------------------------
                //GET OBJECT being dragged
                //var gotObject = actualJSObject from $.data() prop of originating Spot (might need to do this in the drag bind)

                var draggedPiece = ui.draggable.get(0);
                var thisID = "#" + draggedPiece.id;
                var thisSRC = draggedPiece.src;

                //------------------------------------------------------------------

                ui.draggable.remove(); //Got what we need, now delete previous img.
                window._newPiece.CreatePieceIMG(thisID, thisSRC, $(this));

                //look up Spot we just dropped onto and bind .draggable() and .droppable() to newly moved piece Spot

                //This will break if you change the order of script tags in default.aspx
                (new window.BoardTwoD()).AddDragAndDrop('#' + window._draggedPieceID, thisID); //TODO: This is a cheat, But I can't think of a better way to do this recursion at the moment..
            }
        })
        .bind('mousemove', function (e) {
            _thisSpot.GetInfo(e);
        })
        .bind('mouseleave', function () {
            window._debug.ClearMouseDebugBoxes();
        });
    },
    GetID: function (pieceID) {
        //Look at the parent of the <IMG> element passed in

        return $("#" + pieceID).parent().attr('id'); //data
    },
    GetPieceID: function (spotObj) {
        return spotObj.children().first().attr('id'); //.data('id")  //".ui-draggable"
    },
    GetInfo: function (event) {

        //This function is the place where we get the ID of the Spot of the event passed in.
        //at the moment, this info is just passed to the debug object, but eventually it will be
        //passed to the rules object.

        var target = $(event.target);
        var targetID = target.attr('id'); //data
        var isTD = target.is('td');
        var isIMG = target.is('img');

        var spotInfo;
        var pieceInfo = "Not a Piece"; // pieces are smaller than spots.  make sure you are over the piece
        var pieceOnSpot = "Unknown";

        if (isTD && (targetID != undefined)) {

            //We have ourselves a Spot
            spotInfo = targetID;
        }
        else {

            //ok.. so this is *not* a spot.  What are we hovering over then?
            if (isIMG) {
                spotInfo = _thisSpot.GetID(targetID); //ok. fine. get the id of the spot *under* the IMG

                pieceInfo = targetID;
                pieceOnSpot = targetID;
            }

            //These might be encountered when the rules file is not written correctly
            else if (isTD) {
                //Theoretically possible to get here, but it would be an error.  Why are we in a TD and targetID undefined?
                //The board is not set up correctly
                spotInfo = "TD element has no .attr('id') prop. Is the board set up correctly?" + " - ID: " + target.attr('id') + "     ";
            }
            else {
                //Theoretically possible to get here too, but it would be an error.  Where are we.  Did a new feature get added?
                spotInfo = "(?) element has no .attr('id') prop.  Is the board set up correctly?" + " - ID: " + target.attr('id') + "     ";
            }
        }

        //By this point, we should know all there is to know about the spot whose event is being passed in.
        //Do any further operations here

        window._debug.PopulateDebugStuff(spotInfo, pieceInfo, pieceOnSpot);
    },
    AllowDrop: function ($spot) {
        $spot.addClass("ui-droppable");
    },
    DenyDrop: function ($spot) {
        $spot.removeClass("ui-droppable");
    },
    AppendOrReplacePiece: function ($piece, moveIsLegal) {
        var $this = $(this);

        //consider doing this replace maneuver on hover, and drop just finishes the process
        
        ///---------Append/Replace
        //What if there is a piece already there?
        //     if rules.replacePiece, then get rid of the piece that is there
        //         how to get rid? save piece info in variable, because we might need to put it back if rules fail   

        //if !moveIsLegal, then animate a big red X for .5 seconds, then put original piece back
        //     this might eventually go away.  we might just want to keep the red background on hover.

        //Refactor this code snippet from Pieces
//        var    coordinate = rules.PieceDefs[i].coordinate;
//        var name = rules.PieceDefs[i].name;
//        var imageName = rules.PieceDefs[i].imageName;

//        //some pieceDefs are for inheritance only
//        if (coordinate != undefined) {
//            $('td[id="' + coordinate + '"]').append(window._newPiece.PieceIMG_HTML(" " + name, imageName)); //the space is a bugfix for now.
        //        }
        
        // ----append/Replace

    }
});

//    function x (spotID) {
//        //NOT WORKING

//        //Look at the <IMG> child of the spot passed in.  read its ID
//        var pieceInSpot = $('td[id=""' + spotID + '""] .ui-draggable');

//        if (pieceInSpot.length > 0) {
//            alert(pieceInSpot.length);

//            //alert(spot);

//            //            var piece = spot.children()[0];

//            //            if (piece != undefined) {
//            //                if (piece.attr('id') != undefined) {
//            //                    alert(piece.attr('id'));
//            //                };
//            //            }

//            return '';
//        }

//    }