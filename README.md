
# 🃏 Card Game Project

Unity 기반 Card Placement Game  
애니메이션, 사운드, 이펙트가 어우러진 캐주얼 카드 게임입니다.

---

## 📂 Architecture

> 주요 클래스 설계 및 흐름

```
GameManager
├── CardGameController
│   ├── CardPlacementController
│   │   ├── CardPlaceAnimation 
│   │   │   ├── BasicCardPlaceAnimation
│   │   │   ├── ShuffleCardPlaceAnimation
│   │   │   └── BlindCardPlaceAnimation
│   ├── StageController
│   │   └── StageBtn
│   └── SoundManager
├── UI
│   ├── StartPanel
│   ├── EndGameUI
│   └── ScrollController
└── Effect
    ├── SparkleEffect
    └── SparkleObjectPoolManager
```

---

## ✨ 주요 기능

| 기능 | 설명 |
|------|------|
| Game Manager | 게임 전반 로직 관리, 상태 전환 및 컨트롤 |
| Card Placement | 카드 셔플, 블라인드, 기본 배치 애니메이션 지원 |
| Sound Manager | 카드 오픈 / 성공 / 실패 등 효과음 처리 |
| Stage Control | 스테이지 이동 및 상태 관리 |
| Object Pooling | Sparkle 이펙트 풀링 처리로 퍼포먼스 최적화 |
| UI Control | 게임 시작 / 종료 / 스테이지 UI 구성 |

---

## 🎬 Gameplay

> 게임 플레이 사진

<img width="272" alt="스크린샷 2025-04-11 오후 5 37 42" src="https://github.com/user-attachments/assets/bc7bc09e-b2e3-4a19-afde-1d2e7d10a413" />
<img width="267" alt="스크린샷 2025-04-11 오후 5 39 26" src="https://github.com/user-attachments/assets/7272ef58-6788-419e-8b8e-f1a492550f72" />
<img width="271" alt="스크린샷 2025-04-11 오후 5 38 29" src="https://github.com/user-attachments/assets/4d0d8c9f-3108-4564-a3dc-470cd17cb8d8" />
<img width="266" alt="스크린샷 2025-04-11 오후 5 38 35" src="https://github.com/user-attachments/assets/0296abdd-a09e-43f7-9d42-f1a7ddbe3337" />
<img width="262" alt="스크린샷 2025-04-11 오후 5 39 14" src="https://github.com/user-attachments/assets/54d6b1ca-7eaa-4243-80c1-9787242d400e" />


---

## 👥 역할 분담

> 팀원별 담당 영역 및 역할
<img width="1254" alt="image" src="https://github.com/user-attachments/assets/9bbb5182-c001-4069-9e98-e2189249cc3b" />


---
📂 Architecture Diagram

게임 시스템 구조를 한눈에 볼 수 있도록 설계한 아키텍처 다이어그램입니다.
<img width="728" alt="아키텍처다이어그램" src="https://github.com/user-attachments/assets/bb3f644a-d743-4a73-b904-768c75e7954e" />

---

## 🛠️ Tech Stack

- Unity
- C#

---

