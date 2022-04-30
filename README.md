# SoilParamsLib

Library to help estimate parameters of soil water retention models.

### Compile to nupkg

```bash
dotnet build
dotnet pack -c Release --runtime linux-x64
```

### To do

- Account for repetition in MeasuredWaterContents
- Calculate Standard Deviation and Standard Error of each parameter estimative
- Calculate Correlation Matrix of the parameters
