using AutoFixture;
using MeetingsCalenderWPF.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeetingsCalenderWPF.Tests
{
    public class Customizations : ICustomization
    {
        public void Customize(IFixture fixture)
        {
            // Customize the creation of AuthRequest
            fixture.Customize<AuthRequest>(composer =>
                composer.With(request => request.username, "takehome@aircover.ai")
                        .With(request => request.password, "vsdpysMVByK&ir%@iq7*")
                        .With(request => request.magic_token, true));
        }
    }
}
