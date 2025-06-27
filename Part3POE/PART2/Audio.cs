using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Media; // required for soundplayer

namespace Part3POE
{
    public class Audio
    {
        //plays the welcome sound
       public  void PlayWelcomeSound() {
        try
            {
                SoundPlayer player  = new SoundPlayer("C:\\Users\\lab_services_student\\Desktop\\Programming\\CyberSecurity Chatbot\\Audio\\Audio.wav");
                player.Play();
            }
            catch (Exception ex)
            { 
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("[Audio Error: Could not play welcome message]");
                Console.ResetColor();
            }
        }
    }
}
