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

var _thisPiece;
var _moveIsLegal;

//noinspection JSUnusedAssignment
var Piece = Class.extend({
    init: function () {
        _thisPiece = this; //allows us to ref this object inside other local functions
    },
    CreatePieceIMG: function (pieceID, src, spot) {
        spot.append(this.PieceIMG_HTML(pieceID, src));   //set new img
    },
    PieceIMG_HTML: function (pieceID, src) {
        return '<img id="' + pieceID.substring(1) + '" class="thing piece ui-widget-content ui-draggable" src="' + src + '" />'; // - hardcode for now since we don't know what we are doing.
    },
    AddPieceEvents: function (dragSelector) {

        $(dragSelector)
            .draggable({
                revert: "invalid",
                start: function (event, ui) {
                    var thisPieceID = $(this).attr('id');

                    window._lastStartingSpot = _thisPiece.GetSpotID(thisPieceID); //alert(startingSpot);

                    //debug
                    $("#draggedPieceStartingSpot").text(window._lastStartingSpot.toString());
                    $("#draggedPieceDroppedSpot").text(window._lastDroppedSpot + " *picked up*");
                    
                },
                stop: function (event, ui) {
                    //alert("piece stop");
                },
                drag: function (event, ui) {

                    var helper = ui.helper;
                    var helperID = helper.attr('id'); //data

                    window._debug.ShowDraggedInfo(helperID,
                                                  helper.attr('src'));

                    window._draggedPieceID = helperID; //cheat.  this data needs to be stored elsewhere

                    // this z-index is reset on spot.drop because this piece is destroyed and a "fresh" one created. so this setting goes away.
                    $("#" + window._draggedPieceID).css('z-index', '2').addClass("legalMove");
                }
            })
            .droppable({
                //                accept: function (el) {
                //                    return el.hasClass('legalMove');
                //                },
                activeClass: "ui-state-hover",
                hoverClass: 'pieceDropHover',
                over: function (event) {
                    //This is where we check the rules while we are moving a piece.
                    //Not gonna allow drop if move is not legal.  (spot will turn red)

                    var $thisObj = $(this);
                    var thisPieceID = $thisObj.attr('id');

                    var hoveringOverPieceSpotID = _thisPiece.GetSpotID(thisPieceID);

                    //$thisObj.addClass("ui-state-highlight2"); //color piece background for debug purposes

                    window._debug.PieceDroppableDebug(thisPieceID, hoveringOverPieceSpotID);
                    window._debug.PIECESaysPopulatePieceHoverSpotStuff(hoveringOverPieceSpotID, 'get from spot.over.  spot knows when a piece is hovering.  run rules there.');

                    //disable spot droppable
                    //if move is legal -

                    //---------------------refactor to Rules Object when done -------------------------------
                    var pieceDefs = window._config.PieceDefs;

                    //Look up movement for all pieces from the rules file
                    //var allPieceMovements = $.grep(movementRules, function(n) { return n.type == "All"; });
                    var movementRules = pieceDefs.filter(function (rule) {
                        return rule.type == "All";
                    });

                    for (var i = 0; i < movementRules.length; i++) {

                        var thisMovementCheck = new window.Movement();
                        thisMovementCheck.PieceID = window._draggedPieceID;

                        //var currentRuleType = movementRules(i).type; //get this so we can display on screen to the right
                        //display on screen to right. This is a chess variant, nobody's gonna remember the rules! (Remember to blow away this message when you pick up the next piece!)

                        //var currentRuleSuccess = thisMovementCheck.Run(movementRules(i));

                        //ok, so if rule passed, great.  Do nothing. We keep going
                        //If it failed, we need to color square Red, and stop now (break;),  You can't do that. ($thisObj.addClass("ui-state-highlight"); //color piece background)

                    }

                    //This is for getting rules on an individual Piece
                    //                var myMovementRules = pieceDefs.filter(function (rule) {
                    //                    return rule.name == window._draggedPieceID &&
                    //                           rule.Movement == ?;
                    //                });

                    //Look up movement for this piece from the rules file
                    //run ALL movement rules from file (rules.Movement.RunAllRules)
                    //We should find this one there. RULES FILE will call this: $this.Movement.Is_One_of_my_pieces()
                    //---------------------refactor to Rules Object when done-----------------------------------------

                    //expecting back a true or false from rules function
                    _moveIsLegal = true; //_rules.Enforce(_thisPiece);

                    //if move was made illegal during this event
                    if (!_moveIsLegal) {
                        $(thisPieceID).removeClass("legalMove");
                    }
                },
                out: function () {

                    var $thisObj = $(this);
                    $(".legalMove").removeClass("legalMove");

                    //_thisPiece.GetSpot($thisObj.attr('id')).removeClass('ui-state-highlight'); //I'd rather do this, but it ain't working

                    //re-enable spot droppable
                    _thisPiece.AllowDrop($thisObj);

                    window._debug.PIECESaysPopulatePieceHoverSpotStuff('', '');
                    window._debug.PIECESaysPopulatePieceHoverStuff('');
                    window._debug.PIECESaysPopulatePlayerHoverStuff('', '');
                },
                drop: function (event, ui) {

                    var $thisObj = $(this);
                    $(".legalMove").removeClass("legalMove");

                    if (!_moveIsLegal) {
                        event.preventDefault();
                    }

                    //if move wasn't legal, revert draggable position
                    //setter - this works by accident
                    $(_thisPiece).draggable("option", "revert", true);
                }
            })
            .bind('mouseleave', function () {
                //window._debug.ClearPieceDebugBoxes(); //temporary.  delete this bound event
            });
    },
    GetSpot: function (pieceID) {
        //Look at the parent of the <IMG> element passed in

        return $($("#" + pieceID).parent().attr('id'));
    },
    GetSpotID: function (pieceID) {
        //Look at the parent of the <IMG> element passed in

        return $("#" + pieceID).parent().attr('id');
    },
    AllowDrop: function ($piece) {
        $piece.addClass("ui-droppable");
    },
    DenyDrop: function ($piece) {
        $piece.removeClass("ui-droppable");
    }
});