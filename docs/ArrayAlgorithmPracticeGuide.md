# Array Algorithm Practice Guide

## Overview
This workspace now includes tools to help you build muscle memory for array algorithms.

## Files for Practice

### `ArrayAlgorithmScratchPad.cs` (Your Practice Arena)
- **Status**: Ready for your experiments
- **Purpose**: Safe space to practice without breaking tests
- **Features**:
  - Pre-configured template
  - Built-in step logging
  - Example implementations in comments

## How to Practice

### Step 1: Choose an Algorithm

### Step 2: Plan Your Approach
Write your approach in comments first:
```csharp
/*
 * ALGORITHM: Find Smallest Number
 * APPROACH:
 * 1. Initialize smallest = first element
 * 2. Loop through remaining elements
 * 3. If current element < smallest, update smallest
 * 4. Return smallest
 */
```

### Step 3: Implement
Write your code in the `ExecuteAsync` method of `ArrayAlgorithmScratchPad.cs`

### Step 4: Update Metadata
```csharp
public override string Name => "Find Smallest Number";
public override string TimeComplexity => "O(n)";
public override string SpaceComplexity => "O(1)";
```

### Step 5: Test Your Implementation
- Run the application
- Test with different inputs:
  - Small: `[5, 2, 8]`
  - Edge: `[-5, -2, -10]`
  - Single: `[42]`

