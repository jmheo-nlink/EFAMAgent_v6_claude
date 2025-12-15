
# WiX 변환 시작용 가이드 (자동 생성)

- ProductName: EFAM
- ProductVersion: 1.0.0
- Manufacturer: (c) LINK Co., Ltd.
- UpgradeCode: {PUT-UPGRADE-CODE-HERE}

## 1) 파일 수집 (heat.exe)
예시(출력 폴더 기준):
```
heat dir "D:\EFAM\Agent\bin\x64\Release" -cg AppFiles -dr INSTALLFOLDER -gg -srd -sreg -o Files.wxs
```
- `-cg AppFiles`: ComponentGroup Id
- `-dr INSTALLFOLDER`: Product.wxs의 설치 폴더 Id와 일치해야 합니다.
- `-gg`: 고정 GUID 생성
- `-srd`: 루트 디렉터리 생성 생략
- `-sreg`: 레지스트리 하베스트 생략

## 2) 빌드
```
candle Product.wxs Files.wxs
light Product.wxs Files.wxs -ext WixUtilExtension
```
또는 VS2022 WiX 프로젝트에서 빌드하세요.

## 3) 바로가기/레지스트리/서비스
- Shortcut, Registry, Service 관련 항목은 기존 InstallShield 설정을 참조해 WiX 요소로 추가하세요.
- 필요하시다면 제가 .isl에서 해당 테이블을 더 뽑아 WiX 조각을 만들어 드릴 수 있습니다.
