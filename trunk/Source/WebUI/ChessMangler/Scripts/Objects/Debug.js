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

//noinspection JSUnusedAssignment
var Debug = Class.extend({
    init: function () {
    },
    PopulateDroppableDebugStuff: function () {

        $("#lastItemDropped").text($("#currentlyBeingDragged").text());

        $("#idOfItemCurrentlyBeingDragged, #imagePathItemBeingDragged, #classesOfObjectBeingDragged").text("");

        $("#lastSpotDropped").text("I have no idea");

        //Highlight for testing purposes. Later this can be used to highlight "to" and "from" Spots
        //$(this).addClass("ui-state-highlight");
    },
    ClearMouseDebugBoxes: function () {
        $("#spotThatMouseIsBeingHoveredOver").text('');
    },
    ClearPieceDebugBoxes: function () {

        //clear out piece debug boxes
        $("#thisIsAPiece").text('');
        $("#thingPieceHoveringOver").text('');
        $("#pieceOnCurrentSpot").text('');
    },
    PopulateDroppableHoverStuff: function (spotID) {
        $("#thingPieceHoveringOver").text(spotID);
    },
    SPOTSaysPopulateHoverStuff: function (pieceHoverText, spotHoveredText) {
        $("#spotPieceHoveringOver").text(spotHoveredText);
        $("#pieceHoveredOver").text(pieceHoverText);
    },
    SPOTSaysPopulatePlayerHoverStuff: function (playerHoverText, playerDraggedText, isMineText) {
        $("#playerHoveredOver").text(playerHoverText);
        $("#playerDraggedPiece").text(playerDraggedText);
        $("#isPieceMine").text(isMineText);
    },
    PIECESaysPopulatePieceHoverStuff: function (pieceHoverText, pieceSpotHoverText2) {
        $("#pieceSaysPieceHoveredOver").text(pieceHoverText);
    },
    PIECESaysPopulatePieceHoverSpotStuff: function (pieceSpotHoverText, pieceSpotHoverText2) {
        $("#pieceSpotPieceHoveringOver").text(pieceSpotHoverText);
        $("#pieceSpotPieceHoveringOver2").text(pieceSpotHoverText2);
    },
    PIECESaysPopulatePlayerHoverStuff: function (playerPieceHoveredOver, isMineText) {
        $("#pieceSaysPlayer").text(playerPieceHoveredOver);
        $("#pieceIsPieceMine").text(isMineText);
    },
    PopulateDebugStuff: function (spotInfo, pieceInfo, pieceOnSpot) {

        $("#spotThatMouseIsBeingHoveredOver").text(spotInfo);
        $("#thisIsAPiece").text(pieceInfo);
        $("#pieceOnCurrentSpot").text(pieceOnSpot);

    },
    ShowDraggedInfo: function (id, src) {

        $("#idOfItemCurrentlyBeingDragged").text(id);
        $("#imagePathItemBeingDragged").text(src);
        //classesOfObjectBeingDragged
    },
    PopulateERRORInfo: function (errorText) {
        $("#divErrors").text(errorText);
    },
    ShowHoverInfo: function (hoveringOverMyPiece, pieceHoveredOver, thisSpotID) {
                //get player of current piece
        window._draggedPiecePlayer = window._rules.GetPlayerByName(window._draggedPieceID);

        //Get player of what we are hovering over
        window._hoveringOverPlayer = window._rules.GetPlayerByName(pieceHoveredOver);

        var isMyPiece = '';
        if (hoveringOverMyPiece == true) {
            isMyPiece = "Yeah!!";
        } else if (hoveringOverMyPiece == null) {
            isMyPiece = "ummmmm....";
        } else {
            isMyPiece = "No....";
        }

        window._debug.SPOTSaysPopulatePlayerHoverStuff(window._hoveringOverPlayer, window._draggedPiecePlayer, isMyPiece);
        window._debug.SPOTSaysPopulateHoverStuff(pieceHoveredOver, thisSpotID);
    },
    ClearErrorsDiv: function () {
        $("#divErrors").text('');

        window._pieceHoveredOver = '';
        window._debug.SPOTSaysPopulateHoverStuff(_pieceHoveredOver);
        window._debug.SPOTSaysPopulatePlayerHoverStuff('', '', '');
        window._debug.PIECESaysPopulatePlayerHoverStuff('', ''); //this needs to be cleared here too.  

        window._debug.PopulateDroppableDebugStuff();
        window._debug.ClearPieceDebugBoxes();
    },
    PieceDroppableDebug: function (thisPieceID, hoveringOverSpotID) {

        window._debug.PIECESaysPopulatePieceHoverStuff(thisPieceID, hoveringOverSpotID);

        var hoveringOverMyPiece = window._rules.AmIHoveringOverMyPiece(thisPieceID);

        var isMyPiece = '';
        if (hoveringOverMyPiece == true) {
            isMyPiece = "Yeah!!!";
        } else if (hoveringOverMyPiece == null) {
            isMyPiece = "ummmmm.....";
        } else {
            isMyPiece = "No.....";
        }


        //        //---------refactor into rules  ---------------------------------------
        //        //get player of current piece
        //        window._draggedPiecePlayer = window._rules.GetPlayerByName(window._draggedPieceID);

        //        //Get player of what we are hovering over
        //        window._hoveringOverPlayer = window._rules.GetPlayerByName(thisPieceID);

        //        var isMyPiece = '';
        //        if (window._draggedPiecePlayer == window._hoveringOverPlayer) {
        //            isMyPiece = "Yeah!";

        //            //for "easy mode"..
        //            //Color Square red
        //            //Set CSS to Red (remember to set back to it was on dragend!
        //        }
        //        else if (window._hoveringOverPlayer == "none") {
        //            isMyPiece = "ummmmm...";
        //        } else {
        //            isMyPiece = "No...";
        //        }

        //---------refactor ---------------------------------------
        window._debug.PIECESaysPopulatePlayerHoverStuff(window._hoveringOverPlayer, isMyPiece);
    }
});


//
//                $(".thing").not($(this)).removeClass("over");
//                $(this).addClass("over");