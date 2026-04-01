# OrderMatchingEngine
This project implements an **in-memory Order Matching Engine** along with a basic **Position Tracking System**, designed to simulate how trading systems process and match buy/sell orders.

The system focuses on:

* Efficient data structures
* Correct matching logic
* Clean architecture
* Testability

---

## Objectives

* Build a **price-time priority based matching engine**
* Handle order lifecycle: `NEW`, `CANCEL`, `MODIFY`, `PRINT`
* Support **partial fills**
* Maintain **position tracking (netQty, avgPrice)**
* Write **unit tests** for validation
* (Optional) Demonstrate **concurrency using producer-consumer model**

---
### Core Components

#### 1. OrderBook

* Maintains BUY and SELL orders
* Uses:

  * `SortedDictionary<decimal, Queue<Order>>`
* Ensures:

  * **Price priority** (best price first)
  * **Time priority** (FIFO via Queue)

---

#### 2. MatchingEngine

* Processes incoming orders
* Matches orders based on:

  * BUY ≥ SELL condition
* Handles:

  * Partial fills
  * Trade generation

---

#### 3. PositionService

* Tracks:

  * `NetQuantity`
  * `AvgPrice`
* Updates position based on executed trades

---

#### 4. Command Layer (Basic)

* Simulates user input via console
* Supports:

  * NEW
  * CANCEL
  * MODIFY
  * PRINT

---

## Matching Rules

* BUY orders match with lowest SELL
* SELL orders match with highest BUY
* **Price-Time Priority**:

  * Better price first
  * Earlier order first
* **Partial fills allowed**

---

## Data Structures Used
`SortedDictionary` : Maintain sorted price levels   
`Queue` : Maintain time priority (FIFO)
`Dictionary` : Fast lookup for orderId (CANCEL/MODIFY)

---

## Order Lifecycle

### NEW

* Adds order to system
* Attempts matching immediately

### CANCEL

* Removes order using orderId
* Rebuilds queue at price level

### MODIFY

* Implemented as:

  ```
  CANCEL -> NEW
  ```
* Maintains correct priority rules

### PRINT

* Displays current order book

---

## Position Tracking

Tracks trading state using:

* **NetQuantity**

  * Current open position
  * `Buy - Sell`

* **AvgPrice**

  * Weighted average of current holdings
  * Updated only on BUY
  * Reset when position = 0

---

## Unit Testing

Implemented using **xUnit**

### Covered Scenarios:

* Basic matching
* Partial fills
* No-match conditions
* Position updates

Run tests:

```bash
dotnet test
```

