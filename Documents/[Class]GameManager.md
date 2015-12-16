##[Class]GameManager

###변수
- [BoardManager]boardScript
	- 함수 쓰기 위한 인스턴스
- [TetrisGrid]mainGrid
	- 메인 보드
- [TetrisGrid]playerGrid
	- 플레이어 블록 정보
- [TetrisGrid]levelGrid
	- 디자인된 레벨에 대한 정보 있는 보드
- [float]gameSpeed
	- 블록 내려가는 속도(초단위)
- [float]holdTime
	- 버튼 홀드 감지 시간
- [float]_timeCounter
	- gameSpeed를 적용하기 위한 변수
- [float]_holdCounter
	- holdTime를 적용하기 위한 변수



###함수
- 엔드페이즈
	- 페이즈 끝날때 처리하는 함수들을 묶음