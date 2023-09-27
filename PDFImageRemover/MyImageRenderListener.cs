//using PdfSharp.Pdf.Advanced;
//using PdfSharp.Pdf.IO;
//using PdfSharp.Pdf;
using iTextSharp.text.pdf.parser;
//using static PdfSharp.Pdf.PdfDictionary;

namespace PDFImageRemover
{

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
    }
