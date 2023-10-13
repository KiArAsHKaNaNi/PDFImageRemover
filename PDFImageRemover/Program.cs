//using PdfSharp.Pdf.Advanced;
//using PdfSharp.Pdf.IO;
//using PdfSharp.Pdf;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.pdf.parser;
using System.IO;
//using static PdfSharp.Pdf.PdfDictionary;

namespace PDFImageRemover
{
    internal partial class Program
    {
        static void Main(string[] args)
        {
            string inputPdfPath = @""; 
            string outputFolder = @""; 

            if (!Directory.Exists(outputFolder))
            {
                Directory.CreateDirectory(outputFolder);
            }

            using (PdfReader pdfReader = new PdfReader(inputPdfPath))
            {
                for (int pageNumber = 1; pageNumber <= pdfReader.NumberOfPages; pageNumber++)
                {
                    RenderImageFromPage(pdfReader, pageNumber, outputFolder);
                }
            }

        }

        static void RenderImageFromPage(PdfReader pdfReader, int pageNumber, string outputFolder)
        {
            PdfReaderContentParser parser = new PdfReaderContentParser(pdfReader);
            MyImageRenderListener listener = new MyImageRenderListener(outputFolder);

            parser.ProcessContent(pageNumber, listener);
        }
    }
}