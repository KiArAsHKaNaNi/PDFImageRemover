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
    internal class Program
    {
        static void Main(string[] args)
        {
            //System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
            //Console.WriteLine("Hello, World!");
            //DetectAndSaveImages(@"D:\Documents\misc_UserGuide_en.pdf", @"C:\Users\Kiarash\Desktop\sample-pdf-with-images_new2.pdf");

            //string pdfFilePath = @"D:\Documents\start-learning.pdf";
            //string outputFolder = @"D:\PDFToJPG\New4";

            //using (PdfReader pdfReader = new PdfReader(pdfFilePath))
            //{
            //    for (int pageNumber = 1; pageNumber <= pdfReader.NumberOfPages; pageNumber++)
            //    {
            //        RenderImageFromPage(pdfReader, pageNumber, outputFolder);
            //    }
            //}

            string inputPdfPath = @"D:\Documents\Hanging-Protocol-Guide.pdf"; // Replace with your input PDF file path
            string outputFolder = @"D:\PDFToJPG\New7"; // Replace with your desired output folder

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
            //PdfDictionary page = pdfReader.GetPageN(pageNumber);
            //PdfDictionary resources = page.GetAsDict(PdfName.RESOURCES);
            //PdfDictionary xObject = resources.GetAsDict(PdfName.XOBJECT);
            //if (!Directory.Exists(outputFolder))
            //{
            //    Directory.CreateDirectory(outputFolder);
            //}

            //if (xObject != null)
            //{
            //    foreach (PdfName name in xObject.Keys)
            //    {
            //        PdfObject obj = xObject.GetDirectObject(name);
            //        if (obj is PdfStream stream && stream.Get(PdfName.SUBTYPE).Equals(PdfName.IMAGE))
            //        {
            //            byte[] bytes = PdfReader.GetStreamBytesRaw((PRStream)stream);
            //            string outputPath = outputFolder + $@"\{name}.jpg";
            //            File.WriteAllBytes(outputPath, bytes);
            //        }
            //    }
            //}

            PdfReaderContentParser parser = new PdfReaderContentParser(pdfReader);
            MyImageRenderListener listener = new MyImageRenderListener(outputFolder);

            parser.ProcessContent(pageNumber, listener);
        }

        public class MyImageRenderListener : IRenderListener
        {
            private readonly string outputFolder;

            public MyImageRenderListener(string outputFolder)
            {
                this.outputFolder = outputFolder;
            }

            public void BeginTextBlock() { }

            public void EndTextBlock() { }

            public void RenderText(TextRenderInfo renderInfo) { }

            public void RenderImage(ImageRenderInfo renderInfo)
            {
                PdfImageObject image = renderInfo.GetImage();
                if (image == null)
                    return;

                string extension = image.GetFileType();
                if (string.IsNullOrEmpty(extension))
                {
                    extension = "jpg"; // Default to jpg if the extension is not available
                }

                string outputPath = System.IO.Path.Combine(outputFolder, $"{Guid.NewGuid()}.{extension}");

                using (FileStream fs = new FileStream(outputPath, FileMode.Create))
                {
                    byte[] imageBytes = image.GetImageAsBytes();
                    fs.Write(imageBytes, 0, imageBytes.Length);
                }
            }
        }

        //public static void DetectAndSaveImages(string inputFilePath, string outputFilePath)
        //{
        //    // Load the PDF document
        //    PdfDocument document = PdfReader.Open(inputFilePath, PdfDocumentOpenMode.Import);

        //    // Create a new PDF document to store the extracted images
        //    PdfDocument outputDocument = new PdfDocument();

        //    // Iterate through each page of the input document
        //    foreach (PdfPage page in document.Pages)
        //    {
        //        // Get the resources dictionary of the page
        //        PdfDictionary resources = page.Elements.GetDictionary("/Resources");

        //        if (resources != null)
        //        {
        //            // Get the XObject dictionary from the resources dictionary
        //            PdfDictionary xObjects = resources.Elements.GetDictionary("/XObject");

        //            if (xObjects != null)
        //            {
        //                // Iterate through each XObject in the XObject dictionary
        //                foreach (PdfItem item in xObjects.Elements.Values)
        //                {
        //                    // Check if the XObject is an image
        //                    if (item is PdfReference reference && reference.Value is PdfDictionary xObject && xObject.Elements.GetString("/Subtype") == "/Image")
        //                    {
        //                        // Add the image XObject to the output document
        //                        outputDocument.AddPage(page);
        //                        break;
        //                    }
        //                }
        //            }
        //        }
        //    }

        //    // Save the output document with the extracted images
        //    outputDocument.Save(outputFilePath);
        //}

        //public static void DetectAndSaveImages2(string inputFilePath, string outputFilePath)
        //{
        //    // Load the PDF document
        //    PdfDocument document = PdfReader.Open(inputFilePath, PdfDocumentOpenMode.Import);

        //    // Create a new PDF document to store the extracted images
        //    PdfDocument outputDocument = new PdfDocument();

        //    // Iterate through each page of the input document
        //    foreach (PdfPage page in document.Pages)
        //    {
        //        // Get the resources dictionary of the page
        //        PdfDictionary resources = page.Elements.GetDictionary("/Resources");

        //        if (resources != null)
        //        {
        //            // Get the XObject dictionary from the resources dictionary
        //            PdfDictionary xObjects = resources.Elements.GetDictionary("/XObject");

        //            if (xObjects != null)
        //            {
        //                // Iterate through each XObject in the XObject dictionary
        //                foreach (PdfItem item in xObjects.Elements.Values)
        //                {
        //                    // Check if the XObject is an image
        //                    if (item is PdfReference reference && reference.Value is PdfDictionary xObject && xObject.Elements.GetString("/Subtype") == "/Image")
        //                    {
        //                        // Add the image XObject to the output document
        //                        outputDocument.AddPage(page);
        //                        break;
        //                    }
        //                }
        //            }
        //        }
        //    }

        //    // Save the output document with the extracted images
        //    outputDocument.Save(outputFilePath);
        //}
    }
}