##[Class]BoardManger

###기능설명
- [Class]TetrisGrid 에서 적용된 정보를 Scene에 적용(시각화) 하기 위한 함수 묶음
- Scene 업데이트(오브젝트 끄고 키기) 부터 초기화 등 GameManager(또는 TetrisGrid?)에서 호출할 함수들

###변수
- [GameObject] blockTile
	- 테트리스 블록 한칸으로 쓰일 오브젝트
	- 나중에 색깔 별로 적용할 수 있도록 변경
- [Int Array] _boardGrid
	- mainGrid와 달리 화면에 표시된 테트리스 그리드의 정보를 답는다
	- 보드업데이트 함수에서 mainGrid와 비교하여 변경된 값만 적용해 최적화한다. (전체 업데이트 필요없이)
- [GameObject.Transform] _boardHolder
	- 블록 오브젝트들을 child로 묶어 놓는 역할


###함수
- BoardUpdate
	- 변경된 그리드 정보를 받아 오브젝트들을 껏다 켰다한다.
	- input : mainGrid, playerGrid
- BoardSetup
	- 모든 시각화 오브젝트 등 싹 지우고 다시 Instanciate 한다.
	- 게임 재시작 할때 쓰임
	- input : levelGrid
		- 레벨 디자인된 그리드를 받아 초기화후에 적용한다.