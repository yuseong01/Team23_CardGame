<img width="728" alt="클래스다이어그램" src="https://github.com/user-attachments/assets/55d4eb0e-e5aa-45cf-bec6-c1bc74b32229" /># 파랑이 좋겠군..
First Card Game Project 🎮

Overview

Unity 기반 Card Placement Game
카드 배치와 애니메이션, 사운드, 스테이지 컨트롤 등 전반적인 게임 흐름을 제어하는 구조로 설계된 카드 게임 프로젝트입니다.
Architecture

주요 구조
GameManager → CardGameController → CardPlacementController → CardPlaceAnimation 계열  
           ↘ StageController → StageBtn  
           ↘ SoundManager  
           ↘ UI 관리 (StartPanel / EndGameUI)  
Class Diagram
<img width="728" alt="클래스다이어그램" src="https://github.com/user-attachments/assets/2e886cf8-e167-4a06-9c4d-d1523b740639" />

Core Components

Class	역할
GameManager	게임 전체 흐름 관리 (초기화, 상태 관리 등)
CardGameController	카드 게임 진행 로직 처리
CardPlacementController	카드 배치 동작 처리 (애니메이션 포함)
CardPlaceAnimation	카드 애니메이션 추상 클래스
BasicCardPlaceAnimation / BlindCardPlaceAnimation / ShuffleCardPlaceAnimation	카드 배치 방식별 애니메이션 구현체
SoundManager	BGM, SFX 등 사운드 관리
StageController	스테이지 관리 및 이동 처리
StageBtn	스테이지 선택 버튼 처리
StartPanel / EndGameUI	게임 시작 / 종료 UI 처리
Cards	카드 데이터 및 동작 관리
SparkleEffect / SparkleObjectPoolManager	카드 배치 시 연출 효과 관리
ScrollController	스테이지 스크롤 처리
Folder Structure

Assets/
├── Scripts/
│   ├── Core/
│   ├── Card/
│   ├── Stage/
│   ├── UI/
│   ├── Sound/
│   └── Effect/
└── Prefabs/
Tech Stack

Unity 2021.x
C#
Animator / Coroutine 기반 애니메이션 처리
오브젝트 풀링 (SparkleEffect)
상태 기반 GameManager 설계
이벤트 기반 사운드 처리
Features

카드 배치 애니메이션 시스템
스테이지 별 카드 배치 룰 적용
사운드 매니저 통한 동적 사운드 처리
이펙트 풀링으로 최적화
직관적 UI 구성
확장성 고려한 클래스 설계
Run

Unity Project Open
GameScene 실행
Stage 선택 → 카드 배치 → 게임 플레이
Todo

 카드 종류 추가
 멀티플레이 모드 기획
 애니메이션 개선
 사운드 다국어 지원
