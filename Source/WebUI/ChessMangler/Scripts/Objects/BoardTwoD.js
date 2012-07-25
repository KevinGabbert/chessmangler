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

var _thisBoard;

//noinspection JSUnusedAssignment
var BoardTwoD = Class.extend({
    init: function () {
        _thisBoard = this; //allows us to ref this object inside other local functions
    },
    GenerateGrid: function () {

        //Get SpotDef from rules file

        var SpotDef = window._rulesDef.ChessConfig.Board.twoD.SpotDef;
        var SpotTD = "<td>" + "</td>";

        //rowTemplate method
        //get Columns from rules file and create an array corresponding to colums
        //(current rules file has 8.  create array of 8 Spots)

        //This needs to not be numbers.  instead, it should be an array of Spotdef objects
        var arrayOfSpots = [SpotTD, SpotTD, SpotTD, SpotTD, SpotTD, SpotTD, SpotTD, SpotTD];

        //pass array to template
        $("#RowOfSpots").tmpl(arrayOfSpots).appendTo("#divGrid");
    },
    TagDataPropertiesForNow: function () {
        //This is temporary.  GenerateGrid will be what does it.
        //loop through all the TDs and give them a data element that is its coordinate Spot

        //quick and dirty cause I can't think.  This is going away anyway.

        var b = $('.chessnostyle');

        var x = 0;
        var y = 0;
        $('.board tr').each(function () {
            $(this).find('td').each(function () {
                if (!$(this).is(b)) {
                    //$(this).data('id', y + "." + x);
                    $(this).attr('id', 'spot-' + y + '-' + x);
                    x++;
                }
            });

            if (!$(this).is(b)) {
                x = 1;
                y++;
            }
        });

        //Make a .data coordinate that reads A1, A2, etc.
        //$('item').data('coordinate', 52);
        //see? even an object can be added later.  //$('item').data('bar', { myType: 'test', count: 40 });

        return null;
    },
    PopulatePieces: function (pieces) {

        pieces.PopulatePiecesFromRulesFile(window._config);
    },
    TurnOffTextSelectForAllTDs: function () {
        $('td').disableSelection();
    },
    AddDragAndDrop: function (dragSelector, dropSelector) {

        window._newSpot.AddSpotEvents(dropSelector);
        window._newPiece.AddPieceEvents(dragSelector);
    }
});