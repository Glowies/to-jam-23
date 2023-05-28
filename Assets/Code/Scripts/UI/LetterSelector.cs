using UnityEngine;
using TMPro;

namespace UI {
    public class LetterSelector : MonoBehaviour {

        private enum Letter {
            A, B, C, D, E, F, G, H, I, 
            J, K, L, M, N, O, P, Q, R,
            S, T, U, V, W, X, Y, Z, _,
            Zero, One, Two, Three, Four, Five, Six, Seven, Eight, Nine, 
            Heart, Space
        }

        public TMP_Text letterText;
        private Letter _letter = Letter.A;

        private int _letterIndex = 0;

        public void ScrollLetterUp()
        {
            _letterIndex--;
            if (_letterIndex < 0) {
                _letter = Letter.Space;
                _letterIndex = 38;
            } else {
                _letter--;
            }
            
            ChangeLetter(_letter, letterText);
        }
        
        public void ScrollLetterDown()
        {
            _letterIndex++;
            if (_letterIndex > 38) {
                _letter = Letter.A;
                _letterIndex = 0;
            } else {
                _letter++;
            }
            
            ChangeLetter(_letter, letterText);
        }

        void ChangeLetter(Letter selectedLetter, TMP_Text selectedLetterText) {
            switch (selectedLetter) {
                case Letter.A:
                    selectedLetterText.text = "A";
                    break;
                case Letter.B:
                    selectedLetterText.text = "B";
                    break;
                case Letter.C:
                    selectedLetterText.text = "C";
                    break;
                case Letter.D:
                    selectedLetterText.text = "D";
                    break;
                case Letter.E:
                    selectedLetterText.text = "E";
                    break;
                case Letter.F:
                    selectedLetterText.text = "F";
                    break;
                case Letter.G:
                    selectedLetterText.text = "G";
                    break;
                case Letter.H:
                    selectedLetterText.text = "H";
                    break;
                case Letter.I:
                    selectedLetterText.text = "I";
                    break;
                case Letter.J:
                    selectedLetterText.text = "J";
                    break;
                case Letter.K:
                    selectedLetterText.text = "K";
                    break;
                case Letter.L:
                    selectedLetterText.text = "L";
                    break;
                case Letter.M:
                    selectedLetterText.text = "M";
                    break;
                case Letter.N:
                    selectedLetterText.text = "N";
                    break;
                case Letter.O:
                    selectedLetterText.text = "O";
                    break;
                case Letter.P:
                    selectedLetterText.text = "P";
                    break;
                case Letter.Q:
                    selectedLetterText.text = "Q";
                    break;
                case Letter.R:
                    selectedLetterText.text = "R";
                    break;
                case Letter.S:
                    selectedLetterText.text = "S";
                    break;
                case Letter.T:
                    selectedLetterText.text = "T";
                    break;
                case Letter.U:
                    selectedLetterText.text = "U";
                    break;
                case Letter.V:
                    selectedLetterText.text = "V";
                    break;
                case Letter.W:
                    selectedLetterText.text = "W";
                    break;
                case Letter.X:
                    selectedLetterText.text = "X";
                    break;
                case Letter.Y:
                    selectedLetterText.text = "Y";
                    break;
                case Letter.Z:
                    selectedLetterText.text = "Z";
                    break;
                case Letter._:
                    selectedLetterText.text = "_";
                    break;
                case Letter.Zero:
                    selectedLetterText.text = "0";
                    break;
                case Letter.One:
                    selectedLetterText.text = "1";
                    break;
                case Letter.Two:
                    selectedLetterText.text = "2";
                    break;
                case Letter.Three:
                    selectedLetterText.text = "3";
                    break;
                case Letter.Four:
                    selectedLetterText.text = "4";
                    break;
                case Letter.Five:
                    selectedLetterText.text = "5";
                    break;
                case Letter.Six:
                    selectedLetterText.text = "6";
                    break;
                case Letter.Seven:
                    selectedLetterText.text = "7";
                    break;
                case Letter.Eight:
                    selectedLetterText.text = "8";
                    break;
                case Letter.Nine:
                    selectedLetterText.text = "9";
                    break;
                case Letter.Heart:
                    selectedLetterText.text = "♥";
                    break;
                case Letter.Space:
                    selectedLetterText.text = " ";
                    break;
            }
        }
    }
}