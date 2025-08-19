# ✈️ FlyingSystem3D
*A comprehensive Unity simulation featuring realistic jet flight physics, combat systems, and immersive visual effects.*

---

## 🚀 Key Features

### ✈️ Flight Mechanics
- **Basic Torque Control:**
  - Roll, Pitch, and Yaw axis control
  - Max angular velocity limitation
- **Takeoff & Landing System:**
  - Physics-based forward thrust
  - Lift force calculation
  - Wheel-based ground detection
- **Visual Effects:**
  - Jet exhaust plume particles
  - Mountain collision explosions
  - Post-crash "WASTED" screen

### 💥 Combat Systems
- **Rocket Shooting:**
  - Launch Rocket with 3D model
  - Target reticle UI for aiming
  - Collision detection
- **Explosion System:**
  - Impact explosions (full VFX)
  - Fail-safe explosions (reduced VFX)
  - Damage radius configuration
- **Camera Effects:**
  - Perlin noise-based shake
  - Shake intensity varies by explosion distance

### ⚙️ Physics Systems
- **Air Resistance:**
  - 3D drag during flight
  - Ground-only drag when landed
- **Safety Systems:**
  - Terrain collision prevention
  - Fail-safe rocket detonation

---

## 📥 Setup & Usage

### 1. Clone the repo:
```bash
git clone https://github.com/yourusername/jet-flight-sim.git
```

### 2. Open in Unity: Requires Unity 6000.0.24f1 (LTS) or later version

### 3. 🎮 Controls

#### ✈️ Flight Controls
| Control          | Key                 | Action                          |
|------------------|---------------------|---------------------------------|
| **Throttle Up**  | `W`                 | Increase speed                 |
| **Throttle Down**| `S`                 | Decrease speed                 |
| **Pitch Up**     | `↑` (Up Arrow)      | Nose up                        |
| **Pitch Down**   | `↓` (Down Arrow)    | Nose down                      |
| **Roll Left**    | `←` (Left Arrow)    | Bank left                      |
| **Roll Right**   | `→` (Right Arrow)   | Bank right                     |
| **Yaw Left**     | `A`                 | Turn left                      |
| **Yaw Right**    | `D`                 | Turn right                     |

#### 💥 Combat Controls
| Control          | Key                 | Action                          |
|------------------|---------------------|---------------------------------|
| **Fire Rocket**  | `Left Mouse Click`  | Launch targeted rocket         |

#### 🛬 Flight Maneuvers
- **Takeoff:** Increase throttle (`W`) + Pitch up (`↑`) at sufficient speed
- **Landing:** Reduce throttle (`S`) + Gently pitch up (`↑`) near runway to reduce descent rate