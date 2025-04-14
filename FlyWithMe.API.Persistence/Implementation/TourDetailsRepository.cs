using FlyWithMe.API.Domain.DTO.Response;
using FlyWithMe.API.Persistence.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Graph.Models.Security;
using Microsoft.Graph.Models.TermStore;
using Microsoft.Graph.Models;
using Microsoft.Kiota.Abstractions;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Numerics;
using System.Runtime.InteropServices;

namespace FlyWithMe.API.Persistence.Implementation
{
    public class TourDetailsRepository : ITourDetailsRepository
    {
        public TourDetails GetTourDetailsListBasedonPlace(string placeName)
        {
            TourDetails tourDetailsResponses = new TourDetails();
            tourDetailsResponses = GetTourDetails().Where(x => x.PlaceName.ToLower() == placeName.ToLower()).FirstOrDefault();
            tourDetailsResponses.tourDetails = GetTourDestinationDetails().Where(x => x.PlaceName.ToLower() == placeName.ToLower()).ToList();
            return tourDetailsResponses;
        }

        private List<TourDetailsResponse> GetTourDestinationDetails()
        {
            List<TourDetailsResponse> tourDetailsResponses = new List<TourDetailsResponse>
        {
            new TourDetailsResponse
            {
                PlaceName = "Paris",
                Name = "Eiffel Tower",
                ImageUrl = "../images/tours/paris/p-1.jpg",
                Description = "Embark on a journey to the legendary Eiffel Tower and be swept away by its timeless " +
                "charm and extraordinary beauty. Ascend to its majestic heights and witness the breathtaking views of " +
                "the enchanting city of Paris stretching out below. From the intricate ironwork to the awe-inspiring " +
                "panorama, every moment spent at this iconic landmark is a testament to romance and elegance. " +
                "Experience the magic of the Eiffel Tower and create memories that will last a lifetime."
            },
            new TourDetailsResponse
            {
                 PlaceName = "Paris",
                Name = "Louvre Museum",
                ImageUrl = "../images/tours/paris/p-2.jpg",
                Description = "Embark on a captivating journey through the prestigious Louvre Museum, " +
                "where artistic treasures await at every turn, including the world-famous Mona Lisa and a " +
                "myriad of other masterpieces. Delve into the rich history and cultural significance of this " +
                "renowned institution, and immerse yourself in the beauty and creativity that grace its hallowed" +
                " halls. Discover the wonders of the Louvre and unlock the secrets of some of the most celebrated" +
                " works of art in the world."
            },
            new TourDetailsResponse
            {
                 PlaceName = "Paris",
                Name = "Seine River Cruise",
                ImageUrl = "../images/tours/paris/p-3.jpg",
                Description = "Indulge in a leisurely journey along the picturesque Seine River, offering " +
                "a refreshing and enchanting way to admire the beauty of Paris from a distinctive vantage " +
                "point. Drift along the tranquil waters, taking in iconic landmarks like the Eiffel Tower " +
                "and Notre-Dame Cathedral, while soaking in the captivating ambiance of the City of Light." +
                " Experience the magic of a Seine River cruise and let the serene surroundings transport " +
                "you to a realm of relaxation and serenity."
            },
             new TourDetailsResponse
            {
                  PlaceName = "Paris",
                Name = "Notre Dame Cathedral",
                ImageUrl = "../images/tours/paris/p-4.jpg",
                Description = "Immerse yourself in the breathtaking beauty of the iconic Notre Dame Cathedral," +
                " a masterpiece of Gothic architecture that stands as a timeless symbol of Parisian heritage " +
                "and magnificence. Marvel at the intricate details of its majestic facade, the grandeur of its" +
                " soaring spires, and the sublime beauty of its stained glass windows. Step inside this sacred " +
                "sanctuary and feel the weight of history and tradition in every stone. Explore the wonders of " +
                "Notre Dame Cathedral and be transported to a realm of architectural splendor and spiritual serenity."
            },
             new TourDetailsResponse
            {
                PlaceName = "Himalayas",
                Name = "Mount Everest Base Camp",
                ImageUrl = "../images/tours/himalayas/h-1.jpg",
                Description = "Embark on the adventure of a lifetime with a trek to the legendary Mount Everest Base Camp. " +
                "Surrounded by towering peaks and breathtaking landscapes, this iconic journey offers an unparalleled experience " +
                "for adventure seekers and nature lovers. Feel the thrill of being in the heart of the Himalayas as you witness " +
                "glaciers, prayer flags, and serene monasteries on your way to one of the world's most iconic destinations."
            },
            new TourDetailsResponse
            {
                PlaceName = "Himalayas",
                Name = "Valley of Flowers",
                ImageUrl = "../images/tours/himalayas/h-2.jpg",
                Description = "Step into a paradise of vibrant colors at the enchanting Valley of Flowers, where nature blooms in its " +
                "purest form. This UNESCO World Heritage site is home to rare Himalayan flora, breathtaking meadows, and cascading waterfalls. " +
                "Lose yourself in the magical beauty of this serene valley as you walk amidst flowers swaying in the crisp mountain breeze, " +
                "creating an unforgettable spectacle of nature’s wonders."
            },
            new TourDetailsResponse
            {
                PlaceName = "Himalayas",
                Name = "Pangong Lake",
                ImageUrl = "../images/tours/himalayas/h-3.jpg",
                Description = "Witness the mesmerizing beauty of Pangong Lake, a shimmering blue jewel set amidst the rugged Himalayan terrain. " +
                "Stretching across India and Tibet, this high-altitude lake changes colors from turquoise to deep blue, leaving visitors spellbound. " +
                "Feel the tranquility of the untouched surroundings as you breathe in the crisp mountain air and soak in the breathtaking panorama."
            },
            new TourDetailsResponse
            {
                PlaceName = "Himalayas",
                Name = "Rohtang Pass",
                ImageUrl = "../images/tours/himalayas/h-4.jpg",
                Description = "Experience the thrill of adventure at Rohtang Pass, a gateway to the snow-clad peaks of the Himalayas. " +
                "Located at an altitude of 13,050 feet, this stunning mountain pass offers breathtaking views, thrilling snow activities, " +
                "and an unforgettable drive through winding roads. Whether you're marveling at the icy landscapes or indulging in adrenaline-pumping " +
                "sports, Rohtang Pass promises an exhilarating Himalayan escapade."
            },
            new TourDetailsResponse
            {
                PlaceName = "Maldives",
                Name = "Male City Tour",
                ImageUrl = "../images/tours/maldives/m-1.jpg",
                Description = "Discover the vibrant heart of the Maldives with a guided tour of Male, the bustling capital city. " +
                "Explore colorful markets, visit the stunning Grand Friday Mosque, and stroll along the picturesque waterfront. " +
                "Experience the rich culture and heritage of this island nation while enjoying breathtaking views of the Indian Ocean."
            },
            new TourDetailsResponse
            {
                PlaceName = "Maldives",
                Name = "Overwater Bungalows Experience",
                ImageUrl = "../images/tours/maldives/m-2.jpg",
                Description = "Immerse yourself in luxury with a stay at a world-famous overwater bungalow. " +
                "Wake up to the sight of crystal-clear waters stretching to the horizon, and step directly into the ocean from your private deck. " +
                "Experience ultimate relaxation as you enjoy pristine beaches, turquoise lagoons, and stunning sunsets from your floating paradise."
            },
            new TourDetailsResponse
            {
                PlaceName = "Maldives",
                Name = "Scuba Diving at Banana Reef",
                ImageUrl = "../images/tours/maldives/m-3.jpg",
                Description = "Dive into the magical underwater world of the Maldives at Banana Reef, one of the most famous dive sites in the world. " +
                "Swim alongside vibrant coral gardens, schools of tropical fish, and graceful manta rays. Whether you're an experienced diver or a beginner, " +
                "this unforgettable experience will leave you in awe of the Maldives' rich marine biodiversity."
            },
            new TourDetailsResponse
            {
                PlaceName = "Maldives",
                Name = "Sunset Dolphin Cruise",
                ImageUrl = "../images/tours/maldives/m-4.jpg",
                Description = "Set sail on a mesmerizing sunset cruise and witness playful dolphins dancing in the waves. " +
                "Feel the gentle sea breeze as you relax on a luxurious boat, sipping a refreshing drink while the sun dips below the horizon. " +
                "This magical experience offers the perfect blend of adventure, romance, and tranquility in the heart of the Indian Ocean."
            },
            new TourDetailsResponse
            {
                PlaceName = "Munnar",
                Name = "Tea Gardens and Tea Museum",
                ImageUrl = "../images/tours/munnar/m-1.jpg",
                Description = "Step into the breathtaking world of Munnar's tea plantations, where rolling hills are covered in a lush green carpet of tea leaves. " +
                "Visit the Tea Museum to learn about the rich history of tea cultivation and enjoy a refreshing cup of authentic Munnar tea. " +
                "Immerse yourself in the soothing aroma and stunning views that make Munnar a paradise for tea lovers."
            },
            new TourDetailsResponse
            {
                PlaceName = "Munnar",
                Name = "Mattupetty Dam & Lake",
                ImageUrl = "../images/tours/munnar/m-2.jpg",
                Description = "Experience the tranquility of Mattupetty Dam, nestled amidst the rolling hills of Munnar. " +
                "Take a serene boat ride across the lake, surrounded by misty mountains and lush forests. " +
                "This picturesque spot is a haven for nature lovers and photography enthusiasts looking to capture Munnar’s ethereal beauty."
            },
            new TourDetailsResponse
            {
                PlaceName = "Munnar",
                Name = "Eravikulam National Park",
                ImageUrl = "../images/tours/munnar/m-3.jpg",
                Description = "Embark on an adventurous journey through Eravikulam National Park, home to the rare Nilgiri Tahr. " +
                "Trek through mist-clad hills and witness breathtaking panoramic views of Munnar’s landscapes. " +
                "During the blooming season, marvel at the stunning Neelakurinji flowers that paint the hills in a mesmerizing shade of blue."
            },
            new TourDetailsResponse
            {
                PlaceName = "Munnar",
                Name = "Top Station Viewpoint",
                ImageUrl = "../images/tours/munnar/m-4.jpg",
                Description = "Stand above the clouds at Top Station, the highest point in Munnar, offering spectacular views of the Western Ghats. " +
                "This stunning viewpoint is perfect for watching the sunrise over misty valleys, creating an unforgettable experience. " +
                "Breathe in the crisp mountain air and let the beauty of Munnar leave you spellbound."
            },
            new TourDetailsResponse
            {
                PlaceName = "Goa",
                Name = "Baga Beach",
                ImageUrl = "../images/tours/goa/g-1.jpg",
                Description = "Feel the vibrant energy of Baga Beach, one of Goa’s most famous coastal gems. " +
                "Whether you're looking to soak up the sun, indulge in thrilling water sports, or experience Goa's electrifying nightlife, " +
                "Baga Beach offers something for everyone. Enjoy delicious Goan cuisine at beach shacks and witness breathtaking sunsets by the shore."
            },
            new TourDetailsResponse
            {
                PlaceName = "Goa",
                Name = "Fort Aguada",
                ImageUrl = "../images/tours/goa/g-2.jpg",
                Description = "Step back in time at Fort Aguada, a 17th-century Portuguese fort standing tall against the Arabian Sea. " +
                "Marvel at its historic lighthouse, walk along its sturdy walls, and soak in the panoramic views of the coastline. " +
                "This iconic fort is a perfect blend of history and scenic beauty, making it a must-visit in Goa."
            },
            new TourDetailsResponse
            {
                PlaceName = "Goa",
                Name = "Dudhsagar Waterfalls",
                ImageUrl = "../images/tours/goa/g-3.jpg",
                Description = "Witness the majestic Dudhsagar Waterfalls, a spectacular four-tiered cascade surrounded by lush greenery. " +
                "Located on the Goa-Karnataka border, this waterfall is a paradise for nature lovers and adventure seekers. " +
                "Enjoy a trek through the Bhagwan Mahavir Wildlife Sanctuary or take a thrilling jeep safari to experience the full glory of Dudhsagar."
            },
            new TourDetailsResponse
            {
                PlaceName = "Goa",
                Name = "Basilica of Bom Jesus",
                ImageUrl = "../images/tours/goa/g-4.jpg",
                Description = "Explore the spiritual and architectural marvel of the Basilica of Bom Jesus, a UNESCO World Heritage Site. " +
                "This centuries-old church houses the preserved remains of St. Francis Xavier and is a masterpiece of Baroque architecture. " +
                "Admire the intricate carvings and soak in the divine atmosphere of one of Goa’s most revered landmarks."
            },
            new TourDetailsResponse
            {
                PlaceName = "Kashmir",
                Name = "Dal Lake",
                ImageUrl = "../images/tours/kashmir/k-1.jpg",
                Description = "Experience the tranquil beauty of Dal Lake, the heart of Srinagar. " +
                "Glide through the serene waters in a traditional Shikara boat, marvel at the floating gardens, " +
                "and stay in a charming houseboat for an unforgettable experience. Witness the breathtaking sunrise " +
                "as the golden hues reflect on the lake, making it a paradise for nature lovers and photographers alike."
            },
            new TourDetailsResponse
            {
                PlaceName = "Kashmir",
                Name = "Gulmarg",
                ImageUrl = "../images/tours/kashmir/k-2.jpg",
                Description = "Discover the winter wonderland of Gulmarg, a premier ski destination and home to one of the highest cable cars in the world. " +
                "Surrounded by snow-capped peaks and lush meadows, Gulmarg is perfect for adventure seekers and nature enthusiasts. " +
                "Enjoy skiing, snowboarding, or a ride on the Gulmarg Gondola for breathtaking panoramic views of the Himalayas."
            },
            new TourDetailsResponse
            {
                PlaceName = "Kashmir",
                Name = "Pahalgam",
                ImageUrl = "../images/tours/kashmir/k-3.jpg",
                Description = "Immerse yourself in the serene beauty of Pahalgam, also known as the 'Valley of Shepherds.' " +
                "Stroll along the Lidder River, explore lush meadows, and embark on a scenic trek to Aru and Betaab Valley. " +
                "A paradise for nature lovers, Pahalgam is the perfect escape to unwind and soak in Kashmir’s breathtaking landscapes."
            },
            new TourDetailsResponse
            {
                PlaceName = "Kashmir",
                Name = "Sonmarg",
                ImageUrl = "../images/tours/kashmir/k-4.jpg",
                Description = "Step into the 'Meadow of Gold,' where snow-covered peaks, pristine rivers, and alpine flowers create a mesmerizing landscape. " +
                "Sonmarg is the gateway to adventure, offering trekking routes to the Thajiwas Glacier and camping under the star-lit sky. " +
                "A must-visit for thrill-seekers and those looking to explore Kashmir’s untouched beauty."
            }
        };
            return tourDetailsResponses;
        }

        private List<TourDetails> GetTourDetails()
        {
            List<TourDetails> tourDetails = new List<TourDetails>
        {
            new TourDetails
            {
                PlaceName = "Paris",
                VideoURL = "https://www.youtube.com/embed/REDVbTQxMXo",
                MainDescription = @"If you're dreaming of a vacation filled with romance, history, stunning architecture, and world-class cuisine, 
                look no further than Paris! 🌍💖 The City of Light has long been considered one of the most enchanting and sought-after travel destinations in the world, 
                and for good reason! Whether you’re a history buff, art lover, foodie, or a hopeless romantic, Paris offers something extraordinary for everyone. So, why
                should you pack your bags and jet off to this iconic city? Let’s dive in! 🏛️🥖🎭",
                tourDetails = new List<TourDetailsResponse>()
            },
            new TourDetails
            {
                PlaceName = "Himalayas",
                VideoURL = "https://www.youtube.com/embed/vMVzLJ5k7Zc", 
                MainDescription = @"If you are looking for a soul-stirring adventure amidst towering peaks, breathtaking landscapes, and serene monasteries, 
                then the Himalayas should be at the top of your travel list! 🏔️❄️ From the snow-capped mountains to lush green valleys, this magnificent region 
                offers a perfect escape for trekkers, nature lovers, and spiritual seekers alike. Whether it's scaling Everest, exploring the tranquil beauty of Ladakh, 
                or witnessing the sunrise over the Annapurna range, the Himalayas promise an unforgettable experience! 🌄✨",
                tourDetails = new List<TourDetailsResponse>()
            },
                        new TourDetails
            {
                PlaceName = "Maldives",
                VideoURL = "https://www.youtube.com/embed/hCQvPX0DLFM", 
                MainDescription = @"Welcome to the Maldives, a tropical paradise where crystal-clear waters, powdery white-sand beaches, and luxurious overwater bungalows await! 🏝️🌊 
                If you're dreaming of a serene getaway filled with relaxation, adventure, and breathtaking marine life, then the Maldives is the perfect destination. 
                Whether you're diving into vibrant coral reefs, enjoying a romantic sunset cruise, or simply unwinding on a private beach, every moment here feels like a dream. 
                Escape to this island paradise and experience the ultimate blend of tranquility and luxury! ✨🐠🌅",
                tourDetails = new List<TourDetailsResponse>()
            },
                        new TourDetails
            {
                PlaceName = "Munnar",
                VideoURL = "https://www.youtube.com/embed/OiQOFdTKeRM", 
                MainDescription = @"Escape to the mesmerizing hills of Munnar, a paradise nestled in the Western Ghats of Kerala! 🌿🏞️  
                Known for its endless tea plantations, misty mountains, and cool climate, Munnar is the perfect retreat for nature lovers and adventure seekers alike.  
                Take a stroll through lush green valleys, witness breathtaking waterfalls, and sip on freshly brewed tea while soaking in the serene beauty of this hill station.  
                Whether you're trekking to the highest peaks, boating in the tranquil lakes, or just unwinding amidst nature, Munnar offers a peaceful and rejuvenating escape from the bustling city life. 🍃☕✨",
                tourDetails = new List<TourDetailsResponse>()
            },
                        new TourDetails
            {
                PlaceName = "Goa",
                VideoURL = "https://www.youtube.com/embed/ZHqaLQ1OeEg", 
                MainDescription = @"Welcome to Goa – India's beach paradise! 🏖️🌊☀️  
                Whether you're craving **golden sandy beaches, thrilling water sports, buzzing nightlife, or rich Portuguese heritage**, Goa has it all!  
                Relax on the stunning shores of Baga and Palolem, explore the majestic forts of Aguada and Chapora, or dance the night away at vibrant beach parties. 🎶🥂  
                From delicious seafood shacks to tranquil churches and spice plantations, Goa is the perfect blend of **adventure, relaxation, and culture**.  
                Whether you're a solo traveler, honeymooner, or adventure enthusiast, Goa promises an unforgettable experience! 🌴🍹✨",
                tourDetails = new List<TourDetailsResponse>()
            },
                        new TourDetails
            {
                PlaceName = "Kashmir",
                VideoURL = "https://www.youtube.com/embed/rFyO1NvOfsg", 
                MainDescription = @"✨ Welcome to Kashmir – **Paradise on Earth!** ❄️🏔️🌸  
                Nestled in the majestic Himalayas, Kashmir is a dreamland of **snow-capped peaks, lush valleys, and mesmerizing lakes**. 🏞️  
                Take a **shikara ride on the tranquil Dal Lake**, stay in a charming houseboat, or lose yourself in the vibrant beauty of the Mughal Gardens. 🌿💐  
                Whether it's the thrill of skiing in Gulmarg, the serenity of Pahalgam, or the spirituality of Vaishno Devi, Kashmir is an **enchanting escape**.  
                Indulge in delicious Kashmiri cuisine, sip on warm Kahwa tea, and witness the unmatched hospitality of the locals. ☕🥘  
                No matter the season, Kashmir is a **heavenly retreat** that will capture your heart forever! ❤️✨",
                tourDetails = new List<TourDetailsResponse>()
            },





            };
            return tourDetails;
        }
    }
}
