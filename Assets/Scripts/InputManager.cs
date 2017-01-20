using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour {

    public bool inputLeft;
    public bool inputRight;
    public bool inputTurn;
    public bool inputSoftDrop;
    public bool inputHardDrop;

    public float gameSpeed = 1;
    public float holdTime = 0.1f;
    private float _timeCounter;
    private float _RLholdCounter;
    private float _DholdCounter;

    private enum holdKey { Null, Left, Right }
    private holdKey _isHolding;

    // 최종 인풋 계산
    public void UpdateInput() {
        // 상태 초기화
        ResetInput();

        // CurrentMode 가 Manual
        //일정 시간 마다
        if (_timeCounter >= gameSpeed) {
            //카운터 초기화
            _timeCounter = 0f;

            // 블록이 소프트 드랍 시도
            inputSoftDrop = true;
        }

        UpdateHoldingKey();

        if (Input.GetKey(KeyCode.DownArrow)) {
            if (holdTime <= _DholdCounter) {
                inputSoftDrop = true;
                _DholdCounter = 0;
            }
            else
                _DholdCounter += Time.deltaTime;
        }

        if (Input.GetKey(KeyCode.RightArrow) && (_isHolding == holdKey.Right)) {
            if (holdTime <= _RLholdCounter) {
                inputRight = true;
                _RLholdCounter = 0f;
            }
            else
                _RLholdCounter += Time.deltaTime;
        }

        if (Input.GetKey(KeyCode.LeftArrow) && (_isHolding == holdKey.Left)) {
            if (holdTime <= _RLholdCounter) {
                inputLeft = true;
                _RLholdCounter = 0f;
            }
            else
                _RLholdCounter += Time.deltaTime;
        }

        // 회전
        if (Input.GetKeyDown(KeyCode.UpArrow))
            inputTurn = true;
        

        // 박기
        if (Input.GetKeyDown(KeyCode.Space))
            inputHardDrop = true;
        
        _timeCounter += Time.deltaTime;
    }


    private void UpdateHoldingKey() {
        if (Input.GetKeyDown(KeyCode.DownArrow)) {
            //홀딩시간 초기화
            _DholdCounter = holdTime;
        }

        if (Input.GetKeyDown(KeyCode.LeftArrow)) {
            //동시 인풋시 무시
            if (Input.GetKeyDown(KeyCode.RightArrow))
                return;

            //홀딩시간 초기화
            _RLholdCounter = holdTime;

            _isHolding = holdKey.Left;
            return;
        }
        if (Input.GetKeyDown(KeyCode.RightArrow)) {
            //동시 인풋시 무시
            if (Input.GetKeyDown(KeyCode.LeftArrow))
                return;

            //홀딩시간 초기화
            _RLholdCounter = holdTime;

            _isHolding = holdKey.Right;
            return;
        }
        if (Input.GetKeyUp(KeyCode.LeftArrow)) {
            //홀딩시간 초기화
            _RLholdCounter = holdTime;

            if (Input.GetKey(KeyCode.RightArrow)) {
                _isHolding = holdKey.Right;
                return;
            }

            _isHolding = holdKey.Null;
            return;
        }
        if (Input.GetKeyUp(KeyCode.RightArrow)) {
            //홀딩시간 초기화
            _RLholdCounter = holdTime;

            if (Input.GetKey(KeyCode.LeftArrow)) {
                _isHolding = holdKey.Left;
                return;
            }

            _isHolding = holdKey.Null;
            return;
        }
    }

    private void ResetInput() {
        inputLeft = false;
        inputRight = false;
        inputTurn = false;
        inputSoftDrop = false;
        inputHardDrop = false;
    }
}
