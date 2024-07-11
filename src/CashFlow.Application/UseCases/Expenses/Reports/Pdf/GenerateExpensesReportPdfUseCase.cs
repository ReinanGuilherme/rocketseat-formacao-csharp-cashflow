using CashFlow.Application.UseCases.Expenses.Reports.Pdf.Fonts;
using CashFlow.Domain.Repositories.Expenses;
using MigraDoc.DocumentObjectModel;
using PdfSharp.Fonts;
using System.Globalization;

namespace CashFlow.Application.UseCases.Expenses.Reports.Pdf
{
    public class GenerateExpensesReportPdfUseCase : IGenerateExpensesReportPdfUseCase
    {
        private const string CURRENCY_SYMBOL = "€";
        private readonly IExpensesReadOnlyRepository _repository;

        public GenerateExpensesReportPdfUseCase(IExpensesReadOnlyRepository repository)
        {
            _repository = repository;

            GlobalFontSettings.FontResolver = new ExpensesReportFontResolver();
        }

        public async Task<byte[]> Execute(string month)
        {

            DateTime monthFormatted = DateTime.ParseExact(month, "yyyy-MM", CultureInfo.InvariantCulture);

            var expenses = await _repository.FilterByMonth(monthFormatted);
            if (expenses.Count == 0)
            {
                return [];
            }

            var document = CreateDocument("Reinan",monthFormatted);



            return [];
        }

        private Document CreateDocument(string author, DateTime month)
        {
            var document = new Document();

            document.Info.Title = "Titulo documento" + month.ToString("Y");
            document.Info.Author = author;

            var style = document.Styles["Normal"];
            style!.Font.Name = FontHelper.RALEWAY_REGULAR;

            return document;
        }
    }
}
