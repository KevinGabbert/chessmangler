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

var _rulesDef = {
    "ChessConfig": {
        "version": "CM2 pre-alpha",
        "game": "Chess",
        "Board": {
            "twoD": {
                "type": "tetragon",
                "columns": "8",
                "rows": "8",
                "SpotDef": {
                    "coordinate" : "-1",
                    "length" : "100",
                    "width" : "100"
                }
            }
        },
        "PieceDefs": [
            {
                "type": "Starting FEN",
                "FEN": "8/8/p6k/2R5/3p4/P7/1P1rNK2/8 b - - 1 38"
            },
            {
                "type": "Multiple",
                "subtype": "FillRank",
                "FENCode": "P",
                "namePrefix": "WhitePawn",
                "startingaRank": "2",
                "player": "White",
                "imageName": "images/chessPieces/Chess.Merida/wp.png",
                "Movement":[{
                    "condition": {
                         "type": "Can Move"
                     }
                   }, 
                   {
                     "condition": {
                         "type": "Not Out of Bounds"
                   }
                }],
                "Capture": [{
                    "condition": {
                          "type": "Not own Piece"
                    }
                },
                {
                    "condition": {
                          "type": "Replace Opposing Piece"
                    }
                }]
            },
            {
                "name": "WhiteRookLeft",
                "FENCode" : "R",
                "startingSpot": "A1",
                "coordinate": "spot-8-1",
                "player": "White",
                "imageName": "images/chessPieces/Chess.Merida/wr.png"
            },
            {
                "name": "WhiteKnightLeft",
                "startingSpot": "A1",
                "coordinate": "spot-8-2",
                "player": "White",
                "imageName": "images/chessPieces/Chess.Merida/wn.png"
            },
            {
                "name": "WhiteBishopLeft",
                "startingSpot": "A1",
                "coordinate": "spot-8-3",
                "player": "White",
                "imageName": "images/chessPieces/Chess.Merida/wb.png"
            },
            {
                "name": "WhiteQueen",
                "startingSpot": "F1",
                "coordinate": "spot-8-4",
                "player": "White",
                "imageName": "images/chessPieces/Chess.Merida/wq.png"
            },
            {
                "name": "WhiteKing",
                "startingSpot": "E1",
                "coordinate": "spot-8-5",
                "player": "White",
                "imageName": "images/chessPieces/Chess.Merida/wk.png"
            },
            {
                "name": "WhiteBishopRight",
                "startingSpot": "F1",
                "coordinate": "spot-8-6",
                "player": "White",
                "imageName": "images/chessPieces/Chess.Merida/wb.png"
            },
            {
                "name": "WhiteKnightRight",
                "startingSpot": "F1",
                "coordinate": "spot-8-7",
                "player": "White",
                "imageName": "images/chessPieces/Chess.Merida/wn.png"
            },
            {
                "name": "WhiteRookRight",
                "startingSpot": "F1",
                "coordinate": "spot-8-8",
                "player": "White",
                "imageName": "images/chessPieces/Chess.Merida/wr.png"
            },        
            {
                "name": "WhitePawn1",
                "startingSpot": "F1",
                "coordinate": "spot-7-1",
                "player": "White",
                "imageName": "images/chessPieces/Chess.Merida/wp.png"
            },
            {
                "name": "WhitePawn2",
                "startingSpot": "F1",
                "coordinate": "spot-7-2",
                "player": "White",
                "imageName": "images/chessPieces/Chess.Merida/wp.png"
            },
            {
                "name": "WhitePawn3",
                "startingSpot": "F1",
                "coordinate": "spot-7-3",
                "player": "White",
                "imageName": "images/chessPieces/Chess.Merida/wp.png"
            },
            {
                "name": "WhitePawn4",
                "startingSpot": "F1",
                "coordinate": "spot-7-4",
                "player": "White",
                "imageName": "images/chessPieces/Chess.Merida/wp.png"
            },
            {
                "name": "WhitePawn5",
                "startingSpot": "F1",
                "coordinate": "spot-7-5",
                "player": "White",
                "imageName": "images/chessPieces/Chess.Merida/wp.png"
            },
            {
                "name": "WhitePawn6",
                "startingSpot": "F1",
                "coordinate": "spot-7-6",
                "player": "White",
                "imageName": "images/chessPieces/Chess.Merida/wp.png"
            },
            {
                "name": "WhitePawn7",
                "startingSpot": "F1",
                "coordinate": "spot-7-7",
                "player": "White",
                "imageName": "images/chessPieces/Chess.Merida/wp.png"
            },
            {
                "name": "WhitePawn8",
                "startingSpot": "F1",
                "coordinate": "spot-7-8",
                "player": "White",
                "imageName": "images/chessPieces/Chess.Merida/wp.png"
            },
            {
                "name": "BlackPawn1",
                "startingSpot": "F1",
                "coordinate": "spot-2-1",
                "player": "Black",
                "imageName": "images/chessPieces/Chess.Merida/bp.png"
            },
            {
                "name": "BlackPawn2",
                "startingSpot": "F1",
                "coordinate": "spot-2-2",
                "player": "Black",
                "imageName": "images/chessPieces/Chess.Merida/bp.png"
            },
            {
                "name": "BlackPawn3",
                "startingSpot": "F1",
                "coordinate": "spot-2-3",
                "player": "Black",
                "imageName": "images/chessPieces/Chess.Merida/bp.png"
            },
            {
                "name": "BlackPawn4",
                "startingSpot": "F1",
                "coordinate": "spot-2-4",
                "player": "Black",
                "imageName": "images/chessPieces/Chess.Merida/bp.png"
            },
            {
                "name": "BlackPawn5",
                "startingSpot": "F1",
                "coordinate": "spot-2-5",
                "player": "Black",
                "imageName": "images/chessPieces/Chess.Merida/bp.png"
            },
            {
                "name": "BlackPawn6",
                "startingSpot": "F1",
                "coordinate": "spot-2-6",
                "player": "Black",
                "imageName": "images/chessPieces/Chess.Merida/bp.png"
            },
            {
                "name": "BlackPawn7",
                "startingSpot": "F1",
                "coordinate": "spot-2-7",
                "player": "Black",
                "imageName": "images/chessPieces/Chess.Merida/bp.png"
            },
            {
                "name": "BlackPawn8",
                "startingSpot": "F1",
                "coordinate": "spot-2-8",
                "player": "Black",
                "imageName": "images/chessPieces/Chess.Merida/bp.png"
            },
            {
               "name": "BlackRookLeft",
               "startingSpot": "A1",
               "coordinate": "spot-1-1",
               "player": "Black",
               "imageName": "images/chessPieces/Chess.Merida/br.png",
               "Movement": [{
                   "condition": {
                       "type": "Not on own Piece"
                   }
               }]
            },
            {
                "name": "BlackKnightLeft",
                "startingSpot": "A1",
                "coordinate": "spot-1-2",
                "player": "Black",
                "imageName": "images/chessPieces/Chess.Merida/bn.png"
            },
            {
                "name": "BlackBishopLeft",
                "startingSpot": "A1",
                "coordinate": "spot-1-3",
                "player": "Black",
                "imageName": "images/chessPieces/Chess.Merida/bb.png"
            },
            {
                "name": "BlackQueen",
                "startingSpot": "F1",
                "coordinate": "spot-1-4",
                "player": "Black",
                "imageName": "images/chessPieces/Chess.Merida/bq.png"
            },
            {
                "name": "BlackKing",
                "startingSpot": "E1",
                "coordinate": "spot-1-5",
                "player": "Black",
                "imageName": "images/chessPieces/Chess.Merida/bk.png"
            },
            {
                "name": "BlackBishopRight",
                "startingSpot": "F1",
                "coordinate": "spot-1-6",
                "player": "Black",
                "imageName": "images/chessPieces/Chess.Merida/bb.png"
            },
            {
                "name": "BlackKnightRight",
                "startingSpot": "F1",
                "coordinate": "spot-1-7",
                "player": "Black",
                "imageName": "images/chessPieces/Chess.Merida/bn.png"
            },
            {
                "name": "BlackRookRight",
                "startingSpot": "F1",
                "coordinate": "spot-1-8",
                "player": "Black",
                "imageName": "images/chessPieces/Chess.Merida/br.png"
            }
         ]
    }
  }


