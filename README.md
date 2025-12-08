# 2D Player Movement Controller

This project contains a complete 2D player movement controller implemented in Unity using the new Input System.  
The controller includes horizontal movement, jumping, double jumping, gravity control, wall detection, wall sliding, and wall jumping.

## Features

### Horizontal Movement
- Uses Unity's Input System.
- Supports smooth left and right movement.
- Automatic sprite flipping based on movement direction.

### Jump System
- Supports multi-jump, configurable through `maxJump`.
- Variable jump height when the jump button is released early.
- Jump count resets upon touching the ground.

### Gravity Handling
- Customizable gravity values.
- Increased gravity when falling to create a snappier drop.
- Maximum fall speed clamp to avoid unnatural fast falling.

### Ground Detection
- Ground check implemented using `Physics2D.OverlapBox`.
- Configurable ground check position and size.

### Wall Detection
- Wall check implemented using `Physics2D.OverlapBox`.
- Detects when the player is touching a wall.

### Wall Slide
- Player slides down the wall when moving toward it.
- Gravity is reduced during a wall slide.
- Slide speed is limited by `wallSlideSpeed`.

### Wall Jump
- Fully implemented wall jump with directional force.
- Temporary movement lock during wall jump to prevent conflict with horizontal input.
- Timer-based wall jump window for more responsive controls.

## Inspector Parameters

### Movement
- `moveSpeed`: Horizontal movement speed.

### Jumping
- `jumpPower`: Vertical force applied when jumping.
- `maxJump`: Total available jumps.
- `jumpsRemaining`: Internal counter for jump tracking.

### Gravity
- `baseGravity`: Default gravity scale.
- `fallSpeedMultiplier`: Multiplier applied during fall.
- `maxFallSpeed`: Maximum allowed falling velocity.

### Ground Check
- `groundCheckPos`: Transform used as the overlap box origin.
- `groundCheckSize`: Size of the overlap box.
- `groundLayer`: Layer mask for detecting ground.

### Wall Check
- `wallCheckPos`: Transform used for wall detection.
- `wallCheckSize`: Size of the wall detection box.
- `wallLayer`: Layer mask for detecting walls.

### Wall Movement
- `wallSlideSpeed`: Vertical speed while sliding.
- `wallJumpPower`: Horizontal and vertical force applied during a wall jump.
- `wallJumpTime`: Duration for which wall jump control is locked.

## Requirements

- Unity 2021 or later.
- Unity Input System package.
- Player object must contain:
  - `Rigidbody2D` (Dynamic)
  - `Collider2D`
- Optional but recommended:
  - Physics Material 2D with zero friction for better wall slide behavior.

## Setup Instructions

1. Add `PlayerMovement.cs` to the player GameObject.
2. Assign `Rigidbody2D` and set Body Type to Dynamic.
3. Create child transforms for:
   - `groundCheckPos`
   - `wallCheckPos`
4. Assign ground and wall layers in the inspector.
5. Configure parameter values according to your game’s movement feel.
6. Ensure walls and ground have proper colliders and layer setup.

## Debug Tools

Gizmos are drawn in the Scene View to visualize:
- Ground check area (white)
- Wall check area (blue)

These help in adjusting detection sizes and positions accurately.

## Folder Structure Recommendation

