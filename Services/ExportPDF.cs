using iText.IO.Font;
using iText.IO.Font.Constants;
using iText.Kernel.Events;
using iText.Kernel.Font;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using iText.Layout.Properties;
using ListaZadan.DAL;
using ListaZadan.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace ListaZadan.Services
{
    public class ExportPDF
    {
        public static void Export(ListaZadanContext db, string location)
        {
            if (db == null || string.IsNullOrEmpty(location))
            {
                throw new ArgumentNullException();
            }
            List<Zadanie> listaZadan = db.Zadania.Include(z => z.Podzadania).ToList();
            using (var writer = new PdfWriter(File.Open(location, FileMode.OpenOrCreate)))
            {
                using (var pdf = new PdfDocument(writer))
                {
                    using (var document = new Document(pdf))
                    {
                        document.SetTopMargin(65);
                        document.SetBottomMargin(30);

                        var font = PdfFontFactory.CreateFont("C://Windows//Fonts//Arial.ttf", PdfEncodings.IDENTITY_H, true);
                        var h1Font = PdfFontFactory.CreateFont("C://Windows//Fonts//Arial.ttf", PdfEncodings.IDENTITY_H, true);
                        var boldFont = PdfFontFactory.CreateFont("C://Windows//Fonts//Arial.ttf", PdfEncodings.IDENTITY_H, true);

                        document.SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER);
                        document.Add(new Paragraph("Lista zadań")
                            .SetFont(h1Font)
                            .SetFontSize(18)
                            .SetBold());

                        document.SetTextAlignment(iText.Layout.Properties.TextAlignment.LEFT);
                        // dodawanie listy zadań
                        List list = new List()
                            .SetSymbolIndent(12)
                            .SetListSymbol("\u2610")
                            .SetFont(font);
                        foreach (var zadanie in listaZadan)
                        {
                            var element = new ListItem();
                            element.Add(new Paragraph(zadanie.Tresc));
                            if (zadanie.Podzadania.Count() > 0)
                            {
                                List underList = new List()
                                    .SetSymbolIndent(12)
                                    .SetListSymbol("\u2610")
                                    .SetFont(font);
                                foreach (var podzadanie in zadanie.Podzadania)
                                {
                                    underList.Add(new ListItem(podzadanie.opis));
                                }
                                element.Add(underList);
                            }
                            list.Add(element);
                        }
                        document.Add(list);
                    }
                }
            }
        }
    }
}
