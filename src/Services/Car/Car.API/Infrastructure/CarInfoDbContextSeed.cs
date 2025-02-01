namespace Car.API.Infrastructure;
public class CarInfoDbContextSeed(IWebHostEnvironment env, ICsvParser csvParser) : IDbSeeder<CarInfoDbContext>
{
    public async Task SeedAsync(CarInfoDbContext context)
    {
        var contentRootPath = env.ContentRootPath;

        if (!context.CarTypes.Any())
        {
            var typePath = Path.Combine(contentRootPath, "Setup", "car_type.csv");
            var types = csvParser.Parse<CarType>(
                typePath,
                values => new CarType
                {
                    Id = int.Parse(values[0]),
                    Name = values[1]
                });
            context.CarTypes.RemoveRange(context.CarTypes);
            context.CarTypes.AddRange(types);
        }

        if (!context.CarBodyTypes.Any())
        {
            var bodyTypePath = Path.Combine(contentRootPath, "Setup", "car_body_type.csv");
            var bodyTypes = csvParser.Parse<CarBodyType>(
                bodyTypePath,
                values => new CarBodyType
                {
                    Id = int.Parse(values[0]),
                    Name = values[1],
                });
            context.CarBodyTypes.RemoveRange(context.CarBodyTypes);
            context.CarBodyTypes.AddRange(bodyTypes);
        }

        if (!context.CarDriveTypes.Any())
        {
            var driveTypePath = Path.Combine(contentRootPath, "Setup", "car_drive_type.csv");
            var driveTypes = csvParser.Parse<CarDriveType>(
                driveTypePath,
                values => new CarDriveType
                {
                    Id = int.Parse(values[0]),
                    Name = values[1],
                });
            context.CarDriveTypes.RemoveRange(context.CarDriveTypes);
            context.CarDriveTypes.AddRange(driveTypes);
        }

        if (!context.CarTransmissionTypes.Any())
        {
            var transmissionTypePath = Path.Combine(contentRootPath, "Setup", "car_transmission_type.csv");
            var transmissionTypes = csvParser.Parse<CarTransmissionType>(
                transmissionTypePath,
                values => new CarTransmissionType
                {
                    Id = int.Parse(values[0]),
                    Name = values[1],
                });
            context.CarTransmissionTypes.RemoveRange(context.CarTransmissionTypes);
            context.CarTransmissionTypes.AddRange(transmissionTypes);
        }

        if (!context.CarEngineTypes.Any())
        {
            var engineTypePath = Path.Combine(contentRootPath, "Setup", "car_engine_type.csv");
            var engineTypes = csvParser.Parse<CarEngineType>(
                engineTypePath,
                values => new CarEngineType
                {
                    Id = int.Parse(values[0]),
                    Name = values[1],
                });
            context.CarEngineTypes.RemoveRange(context.CarEngineTypes);
            context.CarEngineTypes.AddRange(engineTypes);
        }

        if (!context.CarColors.Any())
        {
            var colorPath = Path.Combine(contentRootPath, "Setup", "car_color.csv");
            var colors = csvParser.Parse<CarColor>(
                colorPath,
                values => new CarColor
                {
                    Id = int.Parse(values[0]),
                    Name = values[1],
                });
            context.CarColors.RemoveRange(context.CarColors);
            context.CarColors.AddRange(colors);
        }

        if (!context.CarConditions.Any())
        {
            var conditionPath = Path.Combine(contentRootPath, "Setup", "car_condition.csv");
            var conditions = csvParser.Parse<CarCondition>(
                conditionPath,
                values => new CarCondition
                {
                    Id = int.Parse(values[0]),
                    Name = values[1],
                });
            context.CarConditions.RemoveRange(context.CarConditions);
            context.CarConditions.AddRange(conditions);
        }

        if (!context.CarExchangeOptions.Any())
        {
            var exchangeOptionPath = Path.Combine(contentRootPath, "Setup", "car_exchange_option.csv");
            var exchangeOptions = csvParser.Parse<CarExchangeOption>(
                exchangeOptionPath,
                values => new CarExchangeOption
                {
                    Id = int.Parse(values[0]),
                    Name = values[1],
                });
            context.CarExchangeOptions.RemoveRange(context.CarExchangeOptions);
            context.CarExchangeOptions.AddRange(exchangeOptions);
        }

        if (!context.CarInteriorColors.Any())
        {
            var interiorColorPath = Path.Combine(contentRootPath, "Setup", "car_interior_color.csv");
            var interiorColors = csvParser.Parse<CarInteriorColor>(
                interiorColorPath,
                values => new CarInteriorColor
                {
                    Id = int.Parse(values[0]),
                    Name = values[1],
                });
            context.CarInteriorColors.RemoveRange(context.CarInteriorColors);
            context.CarInteriorColors.AddRange(interiorColors);
        }

        if (!context.CarInteriorMaterials.Any())
        {
            var interiorMaterialPath = Path.Combine(contentRootPath, "Setup", "car_interior_material.csv");
            var interiorMaterials = csvParser.Parse<CarInteriorMaterial>(
                interiorMaterialPath,
                values => new CarInteriorMaterial
                {
                    Id = int.Parse(values[0]),
                    Name = values[1],
                });
            context.CarInteriorMaterials.RemoveRange(context.CarInteriorMaterials);
            context.CarInteriorMaterials.AddRange(interiorMaterials);
        }

        if (!context.CarBrands.Any())
        {
            var markPath = Path.Combine(contentRootPath, "Setup", "car_mark.csv");
            var marks = csvParser.Parse<CarBrand>(
                markPath,
                values => new CarBrand
                {
                    Id = int.Parse(values[0]),
                    Name = values[1],
                    DateCreate = uint.TryParse(values[2], out var dateCreate) ? dateCreate : default(uint?),
                    DateUpdate = uint.TryParse(values[3], out var dateUpdate) ? dateUpdate : default(uint?),
                    CarTypeId = int.Parse(values[4]),
                    NameRus = values[5],
                });
            context.CarBrands.RemoveRange(context.CarBrands);
            context.CarBrands.AddRange(marks);
        }

        if (!context.CarModels.Any())
        {
            var modelPath = Path.Combine(contentRootPath, "Setup", "car_model.csv");
            var models = csvParser.Parse<CarModel>(
                modelPath,
                values => new CarModel
                {
                    Id = int.Parse(values[0]),
                    CarBrandId = int.Parse(values[1]),
                    Name = values[2],
                    DateCreate = uint.TryParse(values[3], out var dateCreate) ? dateCreate : default(uint?),
                    DateUpdate = uint.TryParse(values[4], out var dateUpdate) ? dateUpdate : default(uint?),
                    CarTypeId = int.Parse(values[5]),
                    NameRus = values[6],
                });
            context.CarModels.RemoveRange(context.CarModels);
            context.CarModels.AddRange(models);
        }

        if (!context.CarSeries.Any())
        {
            var seriePath = Path.Combine(contentRootPath, "Setup", "car_serie.csv");
            var series = csvParser.Parse(
                seriePath,
                values => new CarSerie
                {
                    Id = int.Parse(values[0]),
                    CarModelId = int.Parse(values[1]),
                    CarBodyTypeId = int.Parse(values[2]),
                    DateCreate = uint.TryParse(values[3], out var dateCreate) ? dateCreate : default(uint?),
                    DateUpdate = uint.TryParse(values[4], out var dateUpdate) ? dateUpdate : default(uint?),
                    CarGenerationId = int.TryParse(values[5], out var generationId) ? generationId : default(int?),
                    CarTypeId = int.Parse(values[6])
                });
            context.CarSeries.RemoveRange(context.CarSeries);
            context.CarSeries.AddRange(series);
        }

        /*if (!context.CarOptions.Any())
        {
            var optionPath = Path.Combine(contentRootPath, "Setup", "car_option.csv");
            var options = csvParser.Parse(
                optionPath,
                values => new CarOption
                {
                    Id = int.Parse(values[0]),
                    Name = values[1],
                    ParentId = int.TryParse(values[2], out var parentId) ? parentId : default(int?),
                    DateCreate = uint.Parse(values[3]),
                    DateUpdate = uint.Parse(values[4]),
                    CarTypeId = int.Parse(values[5]),
                });
            context.CarOptions.RemoveRange(context.CarOptions);
            context.CarOptions.AddRange(options);
        }*/

        if (!context.CarGenerations.Any())
        {
            var generationPath = Path.Combine(contentRootPath, "Setup", "car_generation.csv");
            var generations = csvParser.Parse<CarGeneration>(
                generationPath,
                values => new CarGeneration
                {
                    Id = int.Parse(values[0]),
                    Name = values[1],
                    CarModelId = int.Parse(values[2]),
                    YearBegin = values[3],
                    YearEnd = values[4],
                    DateCreate = uint.TryParse(values[5], out var dateCreate) ? dateCreate : default(uint?),
                    DateUpdate = uint.TryParse(values[6], out var dateUpdate) ? dateUpdate : default(uint?),
                    CarTypeId = int.Parse(values[7])
                });
            context.CarGenerations.RemoveRange(context.CarGenerations);
            context.CarGenerations.AddRange(generations);
        }

        if (!context.CarCharacteristics.Any())
        {
            var characteristicPath = Path.Combine(contentRootPath, "Setup", "car_characteristic.csv");
            var characteristics = csvParser.Parse<CarCharacteristic>(
                characteristicPath,
                values => new CarCharacteristic
                {
                    Id = int.Parse(values[0]),
                    Name = values[1],
                    ParentId = int.TryParse(values[2], out var parentId) ? parentId : default(int?),
                    DateCreate = uint.TryParse(values[3], out var dateCreate) ? dateCreate : default(uint?),
                    DateUpdate = uint.TryParse(values[4], out var dateUpdate) ? dateUpdate : default(uint?),
                    CarTypeId = int.Parse(values[5]),
                });
            context.CarCharacteristics.RemoveRange(context.CarCharacteristics);
            context.CarCharacteristics.AddRange(characteristics);
        }

        if (!context.CarModifications.Any())
        {
            var modificationPath = Path.Combine(contentRootPath, "Setup", "car_modification.csv");
            var modifications = csvParser.Parse<CarModification>(
                modificationPath,
                values => new CarModification
                {
                    Id = int.Parse(values[0]),
                    CarSerieId = int.Parse(values[1]),
                    CarModelId = int.Parse(values[2]),
                    Name = values[3],
                    DateCreate = uint.TryParse(values[4], out var dateCreate) ? dateCreate : default(uint?),
                    DateUpdate = uint.TryParse(values[5], out var dateUpdate) ? dateUpdate : default(uint?),
                    CarTypeId = int.Parse(values[6]),
                    CarEngineTypeId = int.TryParse(values[7], out var engineType) ? engineType : default(int?),
                    CarDriveTypeId = int.TryParse(values[8], out var driveType) ? driveType : default(int?),
                    CarTransmissionTypeId = int.TryParse(values[9], out var transmissionType)
                        ? transmissionType
                        : default(int?),
                    EngineCapacity = int.TryParse(values[10], out var engineCapacity) ? engineCapacity : default(int?),
                    EnginePower = int.TryParse(values[11], out var enginePower) ? enginePower : default(int?),
                    FuelConsumptionCombined =
                        decimal.TryParse(values[12], CultureInfo.InvariantCulture, out var fuelConsumption)
                            ? fuelConsumption
                            : default(decimal?),
                    GroundClearance =
                        decimal.TryParse(values[13], CultureInfo.InvariantCulture, out var groundClearance)
                            ? groundClearance
                            : default(decimal?),
                });
            context.CarModifications.RemoveRange(context.CarModifications);
            context.CarModifications.AddRange(modifications);
        }

        if (!context.CarEquipment.Any())
        {
            var equipmentPath = Path.Combine(contentRootPath, "Setup", "car_equipment.csv");
            var equipment = csvParser.Parse<CarEquipment>(
                equipmentPath,
                values => new CarEquipment
                {
                    Id = int.Parse(values[0]),
                    Name = values[1],
                    DateCreate = uint.TryParse(values[2], out var dateCreate) ? dateCreate : default(uint?),
                    DateUpdate = uint.TryParse(values[3], out var dateUpdate) ? dateUpdate : default(uint?),
                    CarModificationId = int.Parse(values[4]),
                    PriceMin = int.TryParse(values[5], out var priceMin) ? priceMin : default(int?),
                    CarTypeId = int.Parse(values[6]),
                    Year = int.TryParse(values[7], out var year) ? year : default(int?),
                });
            context.CarEquipment.RemoveRange(context.CarEquipment);
            context.CarEquipment.AddRange(equipment);
        }

        if (!context.CarCharacteristicValues.Any())
        {
            var characteristicValuePath = Path.Combine(contentRootPath, "Setup", "car_characteristic_value.csv");
            var characteristicValues = csvParser.Parse<CarCharacteristicValue>(
                characteristicValuePath,
                values => new CarCharacteristicValue
                {
                    Id = int.Parse(values[0]),
                    Value = values[1],
                    Unit = values[2],
                    CarCharacteristicId = int.Parse(values[3]),
                    CarModificationId = int.Parse(values[4]),
                    DateCreate = uint.TryParse(values[5], out var dateCreate) ? dateCreate : default(uint?),
                    DateUpdate = uint.TryParse(values[6], out var dateUpdate) ? dateUpdate : default(uint?),
                    CarTypeId = int.Parse(values[7])
                });
            context.CarCharacteristicValues.RemoveRange(context.CarCharacteristicValues);
            context.CarCharacteristicValues.AddRange(characteristicValues);
        }
        
        /*if (!context.CarOptionValues.Any())
        {
            var optionValuePath = Path.Combine(contentRootPath, "Setup", "car_option_value.csv");
            var optionValues = csvParser.Parse(
                optionValuePath,
                values => new CarOptionValue
                {
                    Id = int.Parse(values[0]),
                    IsBase = values[1] == "1",
                    CarOptionId = int.Parse(values[2]),
                    CarEquipmentId = int.Parse(values[3]),
                    DateCreate = uint.Parse(values[4]),
                    DateUpdate = uint.Parse(values[5]),
                    CarTypeId = int.Parse(values[6]),
                });
            context.CarOptionValues.RemoveRange(context.CarOptionValues);
            context.CarOptionValues.AddRange(optionValues);
        }*/

        await context.SaveChangesAsync();
    }
}