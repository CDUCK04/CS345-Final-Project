This tower defense game project integrates player-controlled soldiers, enemy AI, resource management, turret-based defense, and various combat systems within a Unity environment. 
The project leverages object-oriented principles, Unity’s NavMesh for intelligent pathfinding, and Animator Controller for smooth and realistic animations. Each component is modular, making the project adaptable and maintainable.

Key Features
1. Player and AI-Controlled Combat System
Player Character and Control Management: The ControlManager script manages player movement, attacks, and animations, with controls designed to respond dynamically to the game environment.
Enemy AI: Enemies are controlled by the Enemy script, which uses Unity’s NavMesh for pathfinding. Enemies engage in combat with soldiers or the player based on proximity and health.
Turret Defense: The Turret script allows turrets to target and shoot enemies within range and line of sight, adding a layer of defensive strategy.
Soldier Combat: The Soldiers script enables player-controlled soldiers to patrol, chase enemies, and attack. Soldiers prioritize enemy targets, engaging in combat when in range.
2. Resource Management and Recruitment
Coin Collection and Display: The CoinPickUp, CoinCounter, and CoinDisplay scripts manage the player's currency collection and display it on the UI, enabling real-time resource management.
Soldier Recruitment System: The SoldierRecruitment script provides a recruitment interface, allowing players to recruit soldiers of varying levels (simple, superior, advanced) based on available coins.
Turret and Tower Defense Strategy: Players can deploy turrets using resources, strategically positioning them to protect key structures from incoming enemies.
3. Health, Damage, and UI Feedback
Health Bars and Damage Interface: The FloatingHealthBar script displays health bars above units, updating dynamically as health changes, providing players with vital feedback on unit status.
Damage System: Implemented via the IDamagable interface, multiple classes (e.g., Enemy, Soldiers) support a damage interface, making combat interactions consistent and modular.
Pause and Start Menus: PauseMenu and Start scripts manage game flow, allowing players to pause, resume, start a new game, or quit.
4. Animation and Visual Feedback
Animator Control: AnimatorMatcher and Killing manage animations for smooth movement and combat actions, enhancing immersion.
Dynamic UI: The health and coin display updates in real time, providing visual feedback that complements gameplay actions and resource management.
5. Advanced Enemy and Soldier AI
Enemy Behavior: The EnemySpawner script spawns enemies at intervals, while the Enemy script allows them to patrol, chase, and attack based on target proximity and sight.
Soldier AI and Targeting: The Soldiers script equips soldiers with sight and attack ranges, prioritizing the nearest enemies and engaging them when within range.
Core Technical Strengths
Object-Oriented Design: Modular classes (e.g., SoldierRecruitment, Turret, Enemy, ControlManager) allow for easy modification, expansion, and debugging.
Intelligent AI Pathfinding: NavMesh-based pathfinding empowers AI with complex behaviors, like chasing and engaging targets, while responding to player actions.
Resource Management Integration: Seamless coin collection and soldier recruitment empower players to make strategic decisions based on available resources.
Animator Integration: Smooth transitions between movement, attack, and idle states enhance character realism and immersion.
Extensibility: Use of interfaces like IDamagable allows easy addition of new game elements that interact within the combat system.

Credits:
Gameplay, Character control, Soldier AI, Enemy AI Programmed By: Teimruazi Bakuradze
Turret AI, Health System By Jonathan Chung
UI/UX By Nam Nguyen
