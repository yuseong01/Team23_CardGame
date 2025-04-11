
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
│   │   ├── CardPlaceAnimation (추상 클래스)
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
| Card Placement | 카드 셔플, 블라인드, 기본 배치 애니메이션 지원 |
| Sound Manager | 카드 오픈 / 성공 / 실패 등 효과음 처리 |
| Stage Control | 스테이지 이동 및 상태 관리 |
| Object Pooling | Sparkle 이펙트 풀링 처리로 퍼포먼스 최적화 |
| UI Control | 게임 시작 / 종료 / 스테이지 UI 구성 |

---

## 🎬 Gameplay

> 게임 플레이 시연 영상

[![Card Game Play](https://img.youtube.com/vi/영상ID/0.jpg)](https://www.youtube.com/watch?v=영상ID)

---
