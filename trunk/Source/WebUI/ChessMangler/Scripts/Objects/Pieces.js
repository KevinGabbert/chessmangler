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

var _thisPieceSet;

//noinspection JSUnusedAssignment
var Pieces = Class.extend({
    init: function () {
        _thisPieceSet = this; //allows us to ref this object inside other local functions
    },
    PopulatePiecesFromRulesFile: function (rules) {
        var i;
        for (i = 0; i <= window._config.PieceDefs.length - 1; i++) {

            //todo: refactor this.
            var coordinate = rules.PieceDefs[i].coordinate;
            var name = rules.PieceDefs[i].name;
            var imageName = rules.PieceDefs[i].imageName;

            //some pieceDefs are for inheritance only
            if (coordinate != undefined) {
                $('td[id="' + coordinate + '"]').append(window._newPiece.PieceIMG_HTML(" " + name, imageName)); //the space is a bugfix for now.
            }
        }
    }
});