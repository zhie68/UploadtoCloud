            String SelectedText = this.comboBox1.SelectedItem.ToString();
            
            label1.Text = "Login To CloudStorage";
            
          
            progressBar2.Invoke((MethodInvoker)(() => progressBar2.Maximum = 100));
            progressBar2.Invoke((MethodInvoker)(() => progressBar2.Value = 0));
            ChromeDriverService service = ChromeDriverService.CreateDefaultService();
            service.HideCommandPromptWindow = true;

            var options = new ChromeOptions();
            options.AddArgument("--window-position=-32000,-32000");
            options.AddArgument("headless");

          
            var driver = new ChromeDriver(service, options);
       
            driver.Navigate().GoToUrl("url");

            IWebElement userx = driver.FindElement(By.Id("user"));
            IWebElement passwordx = driver.FindElement(By.Id("password"));
            IWebElement loginx = driver.FindElement(By.Id("submit"));
          

            userx.SendKeys("eic2");
            passwordx.SendKeys("kp130");

            loginx.Click();
            driver.Navigate().GoToUrl("URL");
            
            
            driver.Navigate().GoToUrl("URL");
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
            IWebElement tx = driver.FindElement(By.ClassName("icon-add"));
            tx.Click();
            IWebElement droparea = driver.FindElement(By.Id("file_upload_start"));
           
            string selected_file = listBox1.GetItemText(listBox1.SelectedItem);

            droparea.SendKeys("C:\\Local\\" + selected_file);
            progressBar2.Visible = true;
            int value = 0;
            bool compleateFlag = false;
            while (true) // Handle timeout somewhere
            {
                var ajaxIsComplete = (bool)(driver as IJavaScriptExecutor).ExecuteScript("return jQuery.active == 0");
                IWebElement element = driver.FindElement(By.Id("uploadprogressbar"));
                String elementval = element.GetAttribute("aria-valuenow");
                String speedVal = element.GetAttribute("original-title");
                double testX = Convert.ToDouble(elementval);
                int f = (int)Math.Round(testX);
                if (f < 0) f = 0;
                if (f > 0) compleateFlag = true;
                progressBar2.Invoke((MethodInvoker)(() => progressBar2.Value = f));
                progressBar2.CreateGraphics().DrawString(f + "  %", SystemFonts.SmallCaptionFont,
                                   Brushes.Red,
                                   new PointF(progressBar2.Width / 2 - (progressBar2.CreateGraphics().MeasureString(f + "  %",
                                   SystemFonts.DefaultFont).Width / 2.0F),
                                   progressBar2.Height / 2 - (progressBar2.CreateGraphics().MeasureString(f + "  %",
                                   SystemFonts.DefaultFont).Height / 2.0F)));

                label1.Text = speedVal;
                if (ajaxIsComplete)
                    break;
                Thread.Sleep(100);
            }
            if (compleateFlag == true) label1.Text = "Uploading Complete";
            else label1.Text = "Uploading Failed, File Already Exist";

            progressBar2.Visible = false;
            driver.Quit();
