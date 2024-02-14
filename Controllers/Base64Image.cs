﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PM02P012024.Controllers
{
    public class Base64Image : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            ImageSource imageSource = null;

            if (value != null)
            {
                String Base64 = (String)value;
                byte[] fotobyte = System.Convert.FromBase64String(Base64);
                var stream = new MemoryStream(fotobyte);

                imageSource = ImageSource.FromStream(() => stream);
            }
            return imageSource;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
