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

var _pieces = new Pieces();

//noinspection JSUnusedAssignment
var Game = Class.extend({
    init: function () {

    },
    Start: function () {

        var board = new window.BoardTwoD(window._rules);

        //GenerateGrid();
        board.TagDataPropertiesForNow(); //this will be refactored into GenerateGrid
        board.TurnOffTextSelectForAllTDs();

        //Read Database to see if there is a board setup stored (as would be the case in the middle of the game)
        
        board.PopulatePieces(_pieces);
        board.AddDragAndDrop(".ui-draggable", ".ui-droppable");
    }
});