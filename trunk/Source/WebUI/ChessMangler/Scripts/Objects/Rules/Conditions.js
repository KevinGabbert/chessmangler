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
var _captureOwnPiece = null; 
var _replaceOpposingPiece = null;
var _movePiece = null;
var _outOfBounds = null;

//noinspection JSUnusedAssignment

//todo:  upgrade this to be a class with a collection of condition objects in it.
//todo: do this after
var Conditions = Class.extend({
    init: function (pieceID) {

        //if pieceID != undefined
        //        if (arguments.length == 1) {
        //            var x = arguments[0];
        //            
        //            //get rules for just the pieceID
        //            // do something with x and y
        //        } 

        //default values for testing

        //All pieces can move by default. Set a condition "can't move" if piece needs to remain stationary
        //if "Can't move" node is found, then set this to false
        //_movePiece = true;
        //_replaceOpposingPiece = true;
        // _outOfBounds = false;

        // ----------------------------------------- refactor --
        //Get rules from Rules file "PieceDefs" - this should be a global condition:

        //reads rules. loads all into local variables
        var pieceDefItem;  //loops through every rule node in the config JSON Array
        for (pieceDefItem = 0; pieceDefItem <= window._config.PieceDefs.length - 1; pieceDefItem++) {
            var currentRule = window._config.PieceDefs[pieceDefItem];

            var type = currentRule.type; //each rule node must have a "type"
            if (type == "Multiple") {
                //there can only be 1 "Multiple" node, so we will grab the first one.
                //to make life easy, it should be one of the first nodes, before piece definition begins, but it is not necessary.

                //-------------------------- refactor -----
                //read and assign movement conditions
                var movementItem;
                for (movementItem = 0; movementItem <= currentRule.Movement.length - 1; movementItem++) {
                    var currentMovementItem = currentRule.Capture[movementItem];

                    if (currentMovementItem.condition.type == "Can Move") {
                        _movePiece = true;
                    }

                    if (currentMovementItem.condition.type == "Not Out of Bounds") {
                        _outOfBounds = false;
                    }
                }

                //-------------------------- refactor -----

                //-------------------------- refactor -------------------------------------------------------
                //read and assign capture conditions
                var captureItem;
                for (captureItem = 0; captureItem <= currentRule.Capture.length - 1; captureItem++) {
                    var currentCaptureItem = currentRule.Capture[captureItem];

                    if (currentCaptureItem.condition.type == "Not own Piece") {
                        _captureOwnPiece = false;
                    }

                    if (currentCaptureItem.condition.type == "Replace Opposing Piece") {
                        _replaceOpposingPiece = true;
                    }
                }
                //-------------------------- refactor -------------------------------------------------------

                //todo: jump out. we are done
            }
        }
        // ----------------------------------------- refactor --
    },
    CaptureOwnPiece: function () {
        return window._conditions.DefaultTrue(_captureOwnPiece);
    },
    ReplaceOpposingPiece: function () {
        return window._conditions.DefaultTrue(_replaceOpposingPiece);
    },
    MovePiece: function () {
        return window._conditions.DefaultTrue(_movePiece);
    },
    OutOfBounds: function () {
        return window._conditions.DefaultFalse(_outOfBounds);
    },
    DefaultTrue: function (value) {
        return (value != null) ? value : true;
    },
    DefaultFalse: function (value) {
        return (value != null) ? value : false;
    }
});