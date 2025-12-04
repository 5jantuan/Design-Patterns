using System;
using System.Text;
using Pipeline.Core;
using Pipeline.Contexts;
using Pipeline.Functions;
using Pipeline.Steps;
using Pipeline.Interfaces;
using Pipeline.Models;

// ----------------- Демонстрация Pipeline -----------------

Console.WriteLine("=== DEMO: Pipeline framework showcase ===\n");

// 1) Создаём базовый pipeline, который лежит в PipelineFunctions.CreatePipeline()
var basePatientPipeline = PipelineFunctions.CreatePipeline();

// Печатаем структуру базового pipeline через интроспекцию
Console.WriteLine(">>> 1) Base Patient Pipeline (from PipelineFunctions.CreatePipeline):");
Pipeline<PatientContext>.PrintPipeline(basePatientPipeline);
Console.WriteLine();

// 2) Создаём PatientContext и выполняем базовый pipeline
var patientContext = new PatientContext
{
    Patient = new Patient { Name = "John Doe" }
};

Console.WriteLine(">>> 2) Executing base patient pipeline:");
basePatientPipeline.Execute(patientContext);
Console.WriteLine();
Console.WriteLine($"Patient after base pipeline: Name={patientContext.Patient.Name}, Doctor={patientContext.Patient.Doctor?.Name ?? "<no doctor>"}");
Console.WriteLine(new string('-', 60));
Console.WriteLine();

// 3) Демонстрация декораторов/обёрток: PrintStep, LoggingStep, TimingStep
var decoratedPipeline = new Pipeline<PatientContext>();

// Оборачиваем ChangeName -> Timing -> Logging -> Print wrapper
IPipelineStep<PatientContext> changeToLucy =
    new PrintStepAndExecuteStep<PatientContext>(
        new LoggingStep<PatientContext>(
            new TimingStep<PatientContext>(
                new ChangeNamePipelineStep { Name = "Lucy" }
            )
        )
    );

// Оборачиваем PrintPatient -> Timing -> Logging -> Print wrapper
IPipelineStep<PatientContext> printPatientDecorated =
    new PrintStepAndExecuteStep<PatientContext>(
        new LoggingStep<PatientContext>(
            new TimingStep<PatientContext>(
                new PrintPatientPipelineStep()
            )
        )
    );

decoratedPipeline.Steps.Add(changeToLucy);
decoratedPipeline.Steps.Add(printPatientDecorated);

Console.WriteLine(">>> 3) Decorated pipeline (Print / Logging / Timing wrappers):");
Pipeline<PatientContext>.PrintPipeline(decoratedPipeline);
Console.WriteLine("\nExecuting decorated pipeline:");
decoratedPipeline.Execute(patientContext);
Console.WriteLine(new string('-', 60));
Console.WriteLine();

// 4) Chain of Responsibility: шаг, который останавливает pipeline (IsDone)
var stopDemoPipeline = new Pipeline<PatientContext>();

stopDemoPipeline.Steps.Add(new PrintStepAndExecuteStep<PatientContext>(new ChangeNamePipelineStep { Name = "BeforeStop" }));
stopDemoPipeline.Steps.Add(new PrintStepAndExecuteStep<PatientContext>(new StopPipelineStep()));
stopDemoPipeline.Steps.Add(new PrintStepAndExecuteStep<PatientContext>(new ChangeNamePipelineStep { Name = "AfterStop" }));

Console.WriteLine(">>> 4) Chain of Responsibility demo (stop pipeline via ICanStop.IsDone):");
Pipeline<PatientContext>.PrintPipeline(stopDemoPipeline);
Console.WriteLine("\nExecuting stop demo pipeline:");
patientContext.Patient.Name = "StartingName";
stopDemoPipeline.Execute(patientContext);
Console.WriteLine($"\nPatient name after stop demo: {patientContext.Patient.Name} (Expected: 'BeforeStop' and 'AfterStop' didn't run)");
Console.WriteLine(new string('-', 60));
Console.WriteLine();

// 5) Demonstrирование работы с другим контекстом — OrderContext
var orderPipeline = new Pipeline<OrderContext>();
orderPipeline.Steps.Add(new PrintStepAndExecuteStep<OrderContext>(new PrintOrderStep()));

var orderContext = new OrderContext
{
    OrderId = "ORD-2025-001",
    Amount = 199.99m
};

Console.WriteLine(">>> 5) Pipeline with another context type (OrderContext):");
Pipeline<OrderContext>.PrintPipeline(orderPipeline);
Console.WriteLine("\nExecuting order pipeline:");
orderPipeline.Execute(orderContext);
Console.WriteLine(new string('-', 60));
Console.WriteLine();

// 6) Вложенный pipeline: шаг, который исполняет другой pipeline
var outerPipeline = new Pipeline<PatientContext>();

// создаём внутренний pipeline
var innerPipeline = new Pipeline<PatientContext>();
innerPipeline.Steps.Add(new PrintStepAndExecuteStep<PatientContext>(new ChangeNamePipelineStep { Name = "InnerName" }));
innerPipeline.Steps.Add(new PrintStepAndExecuteStep<PatientContext>(new PrintPatientPipelineStep()));

// добавляем в outerPipeline обычный шаг, затем EmbeddedPipelineStep, затем печать
outerPipeline.Steps.Add(new PrintStepAndExecuteStep<PatientContext>(new ChangeNamePipelineStep { Name = "OuterStart" }));
outerPipeline.Steps.Add(new EmbeddedPipelineStep<PatientContext>(innerPipeline));
outerPipeline.Steps.Add(new PrintStepAndExecuteStep<PatientContext>(new PrintPatientPipelineStep()));

Console.WriteLine(">>> 6) Embedded (nested) pipeline demo:");
Pipeline<PatientContext>.PrintPipeline(outerPipeline);
Console.WriteLine("\nExecuting outer pipeline (which runs embedded inner pipeline):");
patientContext.Patient.Name = "Beginning";
outerPipeline.Execute(patientContext);
Console.WriteLine(new string('-', 60));
Console.WriteLine();

// 7) Introspect examples: собрать описание в StringBuilder и вывести
Console.WriteLine(">>> 7) Introspection examples (StringBuilder):");
var sb = new StringBuilder();
sb.AppendLine("Custom introspection output for decoratedPipeline:");
decoratedPipeline.Describe(sb);
sb.AppendLine();
sb.AppendLine("Custom introspection output for outerPipeline:");
outerPipeline.Describe(sb);
Console.WriteLine(sb.ToString());

Console.WriteLine("=== DEMO finished ===");
