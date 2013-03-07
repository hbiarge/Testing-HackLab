// --------------------------------------------------------------------------------------------------------------------
// <copyright file="BeginDaySteps.cs" company="Acheve Solutions">
//   Copyright (c) Hugo Biarge. Todos los derechos reservados.
// </copyright>
// <summary>
//   Defines the BeginDaySteps type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace UI.Test.Steps
{
    using NUnit.Framework;

    using TechTalk.SpecFlow;

    using WatiN.Core;

    [Binding]
    public class BeginDaySteps
    {
        [Binding]
        public class StepDefinitions
        {
            private readonly IE browser;

            public StepDefinitions()
            {
                this.browser = new IE("http://localhost:51768/");
            }

            [Given(@"I'm an authenticated as (.*) with password (.*)")]
            public void GivenIMAnAuthenticatedAsPruebaWithPasswordPrueba(string username, string password)
            {
                this.browser.TextField(Find.ById("UserName")).TypeText(username);
                this.browser.TextField(Find.ById("Password")).TypeText(password);
                this.browser.Button(Find.ById("btnEntrar")).Click();
            }

            [Given(@"there is not a day started")]
            public void GivenThereIsNotADayStarted()
            {
            }

            [When(@"I press the start day button")]
            public void WhenIPressTheStartDayButton()
            {
                this.browser.Button(Find.ById("btnIniciarJornada")).Click();
            }

            [Then(@"the stop day button is showe")]
            public void ThenTheStopDayButtonIsShowe()
            {
                Assert.IsNotNull(this.browser.Button(Find.ById("btnTerminarJornada")));
            }

            [Then(@"the start pause button is showed")]
            public void ThenTheStartPauseButtonIsShowed()
            {
                Assert.IsNotNull(this.browser.Button(Find.ById("btnIniciarPausa")));
            }
        }
    }
}
