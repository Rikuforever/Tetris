##[클래스]TetrisGrid

###변수
- [Array]그리드
- [Enum]블록 종류
	- 0 : 메인 그리드
	- 1 : 
- [Array]회전축 좌표
- [Int]색

###함수
- 유효성검사
	- Input : (베이스)그리드
	- Output : Boolean
- 블록 회전
	- Input : 블록 종류
- 왼쪽 이동
	- Input : (베이스)그리드
- 오른쪽 이동
	- Input : (베이스)그리드
- 아래쪽 이동
	- Input : (베이스)그리드
- 아래 이동
	- Input : (베이스)그리드
- 합치기
	- Input : (베이스)그리드
- NextTimer(일정 시간 지난후)
	- Input : (베이스)그리드
- 새로생성가능체크
	- Output : Boolean
- 새로생성
	- Input : (베이스)그리드
