# Unit Conversion Tool

A RESTful ASP.NET Core Web API that converts numerical values between supported units of measurement.

## Features

- Convert values across **length**, **temperature**, **weight/mass**, and **volume**
- Discover supported units via a dedicated endpoint
- Swagger UI for interactive API exploration
- xUnit tests covering conversion logic and HTTP endpoints

## Prerequisites

- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)

## Getting Started

### Restore and run

```bash
dotnet restore
dotnet run --project src/UnitConversionTool.Api
```

The API starts on:

- HTTPS: `https://localhost:7198`
- HTTP: `http://localhost:5032`

### Run tests

```bash
dotnet test
```

## API Documentation

Swagger UI is available at:

- `https://localhost:7198/swagger`

OpenAPI JSON:

- `https://localhost:7198/swagger/v1/swagger.json`

### POST `/api/convert`

Convert a numeric value from one unit to another.

**Request body**

```json
{
  "value": 10,
  "from": "m",
  "to": "ft"
}
```

**Response**

```json
{
  "inputValue": 10,
  "fromUnit": "m",
  "toUnit": "ft",
  "result": 32.8084,
  "category": "length"
}
```

### GET `/api/units`

Returns all supported units grouped by category.

## Supported Units

| Category    | Units                                         |
| ----------- | --------------------------------------------- |
| Length      | `m`, `km`, `cm`, `mm`, `in`, `ft`, `yd`, `mi` |
| Temperature | `c`, `f`, `k`                                 |
| Weight      | `kg`, `g`, `mg`, `lb`, `oz`                   |
| Volume      | `l`, `ml`, `gal`, `qt`, `pt`, `cup`, `floz`   |

Unit codes are case-insensitive.

## Project Structure

```
UnitConversionTool/
├── src/
│   └── UnitConversionTool.Api/
│       ├── Controllers/     # HTTP endpoints
│       ├── Data/            # Hardcoded unit registry
│       ├── Models/          # Request/response DTOs
│       └── Services/        # Conversion business logic
└── tests/
    └── UnitConversionTool.Tests/
```

## Design Decisions

- **Layered architecture**: controllers delegate to a dedicated `IUnitConversionService`, keeping conversion rules isolated from HTTP concerns.
- **Hardcoded registry**: unit definitions live in `UnitRegistry` for simplicity. The structure supports future replacement with a database or configuration source without changing the API contract.
- **Base-unit strategy**: length, weight, and volume convert through a category base unit. Temperature uses explicit formulas because it is affine rather than proportional.
- **Swagger in all environments**: enabled by default so reviewers and recruiters can explore the API immediately after startup.

## Example

```bash
curl -X POST https://localhost:7198/api/convert \
  -H "Content-Type: application/json" \
  -d "{\"value\":100,\"from\":\"c\",\"to\":\"f\"}"
```

Expected result: `212` (Fahrenheit).
