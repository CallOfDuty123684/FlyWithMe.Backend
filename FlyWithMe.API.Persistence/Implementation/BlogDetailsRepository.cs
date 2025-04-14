using FlyWithMe.API.Domain.DTO.Response;
using FlyWithMe.API.Persistence.Interfaces;

namespace FlyWithMe.API.Persistence.Implementation
{
    public class BlogDetailsRepository : IBlogDetailsRepository
    {
        public BlogDetails GetBlogDetailsListBasedonBlogId(string blogId)
        {
            BlogDetails BlogDetailsResponses = new BlogDetails();
            BlogDetailsResponses = GetBlogDetails().Where(x => x.BlogId.ToLower() == blogId.ToLower()).FirstOrDefault();
            if (BlogDetailsResponses != null)
            {
                BlogDetailsResponses.blogDetails = GetTourDestinationDetails().Where(x => x.BlogId.ToLower() == blogId.ToLower()).ToList();
            }
            return BlogDetailsResponses;
        }

        private List<BlogDetailsResponse> GetTourDestinationDetails()
        {
            List<BlogDetailsResponse> blogDetailsResponses = new List<BlogDetailsResponse>
               {
                   new BlogDetailsResponse
                   {
                       BlogId = "Blog1",
                       Name = "Faroe Islands",
                       ImageUrl = "../images/blog/blog1/faroe_islands.jpg",
                       Description = "Nested between Norway and Iceland, the Faroe Islands feature breathtaking cliffs, tumbling waterfalls, " +
                       "and traditional villages spared from large-scale tourism. If you hike the rough trails, see puffins at Mykines, or " +
                       "bask in the sweeping vistas of Sørvágsvatn, the Faroe Islands are heaven for nature enthusiasts."
                   },
                   new BlogDetailsResponse
                   {
                       BlogId = "Blog1",
                       Name = "Luang Prabang",
                       ImageUrl = "../images/blog/blog1/luang_prabang.jpg",
                       Description = "This UNESCO-listed town in northern Laos is a cultural gem filled with ancient temples, vibrant night markets, and serene waterfalls. " +
                       "Explore the Kuang Si Falls, take part in the morning alms-giving ceremony, or cruise along the Mekong River for an unforgettable adventure."
                   },
                   new BlogDetailsResponse
                   {
                       BlogId = "Blog1",
                       Name = "Kotor",
                       ImageUrl = "../images/blog/blog1/kotor.jpg",
                       Description = "Too often overshadowed by its better-known neighbors, Montenegro's Kotor is a medieval wonder. " +
                       "Nestled among fjord-like scenery, its cobblestone streets, ancient fortifications, and spectacular bay vistas make it a paradise for history and nature lovers."
                   },
                   new BlogDetailsResponse
                   {
                       BlogId = "Blog1",
                       Name = "Svaneti",
                       ImageUrl = "../images/blog/blog1/svaneti.jpg",
                       Description = "Hidden away in the Caucasus Mountains, Svaneti is an off-the-tourist-path paradise for trekkers and history buffs alike. " +
                       "With its medieval stone towers, snow-covered summits, and traditional Georgian hospitality, this region offers an ideal combination of adventure and culture."
                   },
                   new BlogDetailsResponse
                   {
                       BlogId = "Blog1",
                       Name = "Raja Ampat",
                       ImageUrl = "../images/blog/blog1/raja_ampat.jpg",
                       Description = "For fans of sea adventures, Raja Ampat is a dream come true. Situated in the center of the Coral Triangle, this Indonesian archipelago " +
                       "boasts some of the world's most diverse coral reefs. Snorkeling and diving here feels like stepping into an aquarium teeming with colorful marine life."
                   },
                   new BlogDetailsResponse
                   {
                       BlogId = "Blog1",
                       Name = "Albarracín",
                       ImageUrl = "../images/blog/blog1/albarracin.jpg",
                       Description = "Turn back the clock in the fairy tale town of Albarracín. Its medieval buildings, rose-tinted walls, and winding cobblestone streets " +
                       "make this Spanish hidden gem a delight away from the tourist crowds. Stroll through its beautifully preserved fortress and take in breathtaking views."
                   },
                   new BlogDetailsResponse
                   {
                       BlogId = "Blog1",
                       Name = "Lofoten Islands",
                       ImageUrl = "../images/blog/blog1/lofoten_islands.jpg",
                       Description = "The Lofoten Islands are heaven for photography enthusiasts, adventurers, and isolation seekers. " +
                       "Famous for their rugged mountains, fishing towns, and the chance to witness the Northern Lights, they offer an untamed, magical experience like no other."
                   },
                   new BlogDetailsResponse
                   {
                       BlogId = "Blog2",
                       Name = "Cultural Festivals That You Won't Want to Miss",
                       ImageUrl = "../images/blog/blog2/b2-1.jpg",
                       Description = "Attending a cultural festival is one of the best ways to witness a country’s heritage come to life. Here are a few of the world’s most spectacular festivals." +
                       " Carnival (Brazil) – A dazzling spectacle of samba, parades, and elaborate costumes, Rio de Janeiro’s Carnival is a celebration like no other." +
                       " Diwali (India) – The festival of lights symbolizes the triumph of good over evil, celebrated with fireworks, sweets, and beautiful lamp-lit decorations." +
                       " Oktoberfest (Germany) – Munich’s world-famous beer festival offers traditional Bavarian food, music, and, of course, liters of beer." +
                       " Cherry Blossom Festival (Japan) – A breathtaking celebration of nature, with hanami (flower viewing) parties held under the stunning pink blossoms." +
                       " Day of the Dead (Mexico) – A vibrant tradition honoring loved ones who have passed, featuring altars, marigolds, and festive parades."
                   },
                   new BlogDetailsResponse
                   {
                       BlogId = "Blog2",
                       Name = "Embark on a Food Tour",
                       ImageUrl = "../images/blog/blog2/b2-2.jpg",
                       Description = "Food is a universal language that connects people to their culture. Here are some top destinations for immersive food tours:" +
                       " Bangkok, Thailand – Discover street food heaven with must-try dishes like pad thai, mango sticky rice, and spicy papaya salad." +
                       " Istanbul, Turkey – Embrace the juxtaposition of East and West via treats such as kebabs, baklava, and steaming Turkish tea." +
                       " Rome, Italy – Savor old-school Italian favorites, from handmade pasta and pizzas cooked in a wood-fired oven to rich gelato." +
                       " Marrakech, Morocco – Experience a color and scent feast at the souks, tucking into tagine, couscous, and warm-out-of-the-oven Moroccan bread." +
                       " New Orleans, USA – Immerse yourself in Cajun and Creole cuisine with gumbo, jambalaya, and beignets in this culinary heaven. "
                   },
                    new BlogDetailsResponse
                   {
                       BlogId = "Blog2",
                       Name = "Authentic Local Experiences",
                       ImageUrl = "../images/blog/blog2/b2-3.jpg",
                       Description = "Immersing oneself in local traditions can give one a richer appreciation of a place. These are some of the ways to live like a local and see culture beyond the typical tourist sights:" +
                       " Stay with a Local Family – Homestays enable you to live life as locals do, with local customs and home-cooked food." +
                       " Take a Traditional Cooking Class – Learn how to make genuine dishes while gaining insight into the cultural importance of every ingredient." +
                       " Participate in a Cultural Workshop – From Balinese dance lessons to Peruvian weaving workshops, hands-on experiences provide authentic insights into an area's culture." +
                       " Visit Local Markets – Markets are the pulse of a society, providing an insight into the local way of life, crafts, and flavors." +
                       " Take Part in Traditional Rituals – Whether a tea ceremony in Japan or an African tribal festival, observing or participating in traditional rituals can be a life-changing experience."
                   },
                     new BlogDetailsResponse
                   {
                       BlogId = "Blog2",
                       Name = "Final Thoughts",
                       ImageUrl = "../images/blog/blog2/b2-4.jpg",
                       Description = "Traveling isn't merely visiting new destinations—it's immersing yourself in the essence of a place by way of its customs, " +
                       "tastes, and locals. By experiencing cultural festivals, sampling food tours, and sharing authentic local encounters, you can form memories" +
                       " to last a lifetime and enhance your love for the cultures of our world. So pack your luggage, open your heart, and be prepared for an " +
                       "experience that is more than mere sightseeing!"
                   },
                      new BlogDetailsResponse
                   {
                       BlogId = "Blog3",
                       Name = "Spectacular Locations",
                       ImageUrl = "../images/blog/blog3/b3-1.jpg",
                       Description = "Luxury resorts are usually located in the most breathtaking places, providing unparallel views and isolation." +
                       " From the overwater villas in the Maldives to the secluded safari camps in South Africa, these getaways promise that visitors" +
                       " rise up to enchanting vistas and serenity."
                   },
                      new BlogDetailsResponse
                   {
                       BlogId = "Blog3",
                       Name = "World-Class Accommodations",
                       ImageUrl = "../images/blog/blog3/b3-2.jpg",
                       Description = "These resorts cut no corners when it comes to building luxurious stays. Imagine opulent suites complete with " +
                       "private infinity pools, fine-thread linens, bespoke butler service, and décor by international design gurus. No detail is" +
                       " too small to indulge in a life of luxury."
                   },
                       new BlogDetailsResponse
                   {
                       BlogId = "Blog3",
                       Name = "Fine Dining Experiences",
                       ImageUrl = "../images/blog/blog3/b3-3.jpg",
                       Description = "Fine dining is a highlight for any luxury resort. Resorts might have Michelin-starred restaurants, " +
                       "beachside dining with privacy, and farm-to-table offerings by master chefs. Guests can indulge in gastronomic" +
                       " delights amidst stunning landscapes."
                   },
                        new BlogDetailsResponse
                   {
                       BlogId = "Blog3",
                       Name = "Unparalleled Wellness & Spa Treatments",
                       ImageUrl = "../images/blog/blog3/b3-4.jpg",
                       Description = "The deluxe resorts provide amenities of international wellness standards, from ancient-inspired treatments " +
                       "in spas, holistic wellness workshops, to stimulating therapies. picture yourself relaxing over a hot-stone massage and a " +
                       "stunning sea view or learning sunrise yoga amidst a cliff-perched retreat."
                   },
                        new BlogDetailsResponse
                   {
                       BlogId = "Blog3",
                       Name = "Private Activities & Customized Services",
                       ImageUrl = "../images/blog/blog3/b3-5.jpg",
                       Description = "From helicopter sightseeing and yacht excursions to tailor-made cultural encounters, all of these resorts excel " +
                       "in going the extra mile to provide something truly special at a guest's own request. Every aspect of the stay is personally " +
                       "tailored and organized by their concierge to ensure an extraordinary experience."
                   },
                         new BlogDetailsResponse
                   {
                       BlogId = "Blog3",
                       Name = "Sustainable Luxury",
                       ImageUrl = "../images/blog/blog3/b3-6.jpg",
                       Description = "Numerous upscale resorts now include sustainability as part of their luxury packages. Environmentally friendly villas, " +
                       "local materials, and conservation efforts enable visitors to indulge in luxury while preserving the environment."
                   },
                          new BlogDetailsResponse
                   {
                       BlogId = "Blog3",
                       Name = "Conclusion",
                       ImageUrl = "../images/blog/blog3/b3-7.jpg",
                       Description = "An opulent retreat is more than just a vacation—it’s a once-in-a-lifetime experience where every detail is designed " +
                       "for ultimate relaxation and indulgence. Whether seeking adventure, tranquility, or cultural immersion, luxury resorts provide the " +
                       "perfect setting to create cherished memories. If you’re ready to elevate your travel experiences, these exclusive retreats await your arrival."
                   },
                          new BlogDetailsResponse
                   {
                       BlogId = "Blog4",
                       Name = "Select Low-Cost Destinations",
                       ImageUrl = "../images/blog/blog4/b4-1.jpg",
                       Description = "Some places are just inherently less expensive to live, and so are best suited for the budget traveler. Try visiting such places as:" +
                       " Southeast Asia (Thailand, Vietnam, Cambodia, Indonesia): Low-cost accommodations, street food, and cheap transport make these nations budget-friendly." +
                       " Eastern Europe (Poland, Romania, Bulgaria): Lovely architecture, historic heritage, and decent prices compared to Western Europe." +
                       " South America (Peru, Bolivia, Colombia): Breathtaking landscapes, tasty cuisine, and low-cost accommodation."
                   },
                          new BlogDetailsResponse
                   {
                       BlogId = "Blog4",
                       Name = "Travel During Off-Peak Seasons",
                       ImageUrl = "../images/blog/blog4/b4-2.jpg",
                       Description = "Avoid traveling during peak tourist seasons to save a lot of money on flights, accommodation, and activities. " +
                       "Shoulder seasons (spring and autumn) usually have good weather and fewer tourists while maintaining affordability."
                   },
                           new BlogDetailsResponse
                   {
                       BlogId = "Blog4",
                       Name = "Use Budget Airlines and Public Transportation",
                       ImageUrl = "../images/blog/blog4/b4-3.jpg",
                       Description = "Rather than flying with large carriers, use low-cost carriers such as Ryanair, AirAsia, or Spirit Airlines. Also, taking the " +
                       "train, bus, or local transport rather than taxis or rental vehicles saves a significant amount of money."
                   },
                           new BlogDetailsResponse
                   {
                       BlogId = "Blog4",
                       Name = "Look for Low-Cost Accommodation",
                       ImageUrl = "../images/blog/blog4/b4-4.jpg",
                       Description = "Be creative in finding alternatives to high-cost hotels, including:" +
                       " Hostels: Ideal for solo travelers and cost-conscious travelers." +
                       " Guesthouses & B&Bs: Inexpensive and provide a more home-like atmosphere." +
                       " House Sitting & Couchsurfing: Stay for free in return for pet sitting or via hospitality networks."
                   },
                             new BlogDetailsResponse
                   {
                       BlogId = "Blog4",
                       Name = "Eat Like a Local",
                       ImageUrl = "../images/blog/blog4/b4-5.jpg",
                       Description = "Avoid tourist restaurants and dine where the locals dine. Street food, markets, " +
                       "and small family-owned restaurants offer delectable food at a fraction of the price."
                   },
                              new BlogDetailsResponse
                   {
                       BlogId = "Blog4",
                       Name = "Take Advantage of Free Activities",
                       ImageUrl = "../images/blog/blog4/b4-6.jpg",
                       Description = "Most places have free or cheap attractions to offer, including:" +
                       " Museums with free admission days" +
                       " Parks, beaches, and hiking trails" +
                       " Walking tours and cultural festivals"
                   },
                               new BlogDetailsResponse
                   {
                       BlogId = "Blog4",
                       Name = "Book and Plan in Advance",
                       ImageUrl = "../images/blog/blog4/b4-7.jpg",
                       Description = "Flights, hotels, and tours booked in advance can result in considerable" +
                       " savings. Make use of price comparison sites and subscribe to fare notices to see the " +
                       "best offers."
                   },
                                 new BlogDetailsResponse
                   {
                       BlogId = "Blog4",
                       Name = "Take Advantage of Travel Reward Programs",
                       ImageUrl = "../images/blog/blog4/b4-8.jpg",
                       Description = "Join airline miles programs, hotel loyalty programs, and travel reward credit cards." +
                       " These may pay for your flights, hotel stays, and other benefits in the long run."
                   },
                                  new BlogDetailsResponse
                   {
                       BlogId = "Blog4",
                       Name = "Final Thoughts",
                       ImageUrl = "../images/blog/blog4/b4-9.jpg",
                       Description = "Seeing the world on a budget is indeed achievable with proper planning. " +
                       "Picking affordable locations, employing astute travel planning, and diving into local" +
                       " encounters will make it possible for you to have irreplaceable travels without breaking" +
                       " your bank. Pack your bags now and prepare yourself for your next cheap adventure!"
                   },
                                     new BlogDetailsResponse
                   {
                       BlogId = "Blog5",
                       Name = "Pack Smart and Light",
                       ImageUrl = "../images/blog/blog5/b5-1.jpg",
                       Description = "One of the greatest travel hacks is to pack smart. Roll your clothes to " +
                       "save space and prevent wrinkles. Use packing cubes to stay organized with your bags. " +
                       "Pack a versatile wardrobe with match-and-mix clothes to keep luggage weight low."
                   },
                                       new BlogDetailsResponse
                   {
                       BlogId = "Blog5",
                       Name = "Pack Crucial Travel Documents in Digital Form",
                       ImageUrl = "../images/blog/blog5/b5-2.jpg",
                       Description = "It is a nightmare to lose vital documents. Store copies of your passport," +
                       " visa, itinerary, and vital contacts on your phone or cloud storage. A backup can be a " +
                       "lifesaver during emergencies."
                   },
                                       new BlogDetailsResponse
                   {
                       BlogId = "Blog5",
                       Name = "Select the Best Flight Deals",
                       ImageUrl = "../images/blog/blog5/b5-3.jpg",
                       Description = "Airfare changes often, so check price comparison sites such as Skyscanner " +
                       "or Google Flights for the best prices. Price alerts can inform you when tickets are discounted." +
                       " Flying on weekdays or in off-seasons usually means cheaper fares."
                   },
                                       new BlogDetailsResponse
                   {
                       BlogId = "Blog5",
                       Name = "Prevent Jet Lag with Simple Adjustments",
                       ImageUrl = "../images/blog/blog5/b5-4.jpg",
                       Description = "To reduce jet lag, reschedule your sleep a few days prior to leaving. Drink plenty of water, " +
                       "don't overindulge in caffeine and alcohol, and attempt to get some sunlight when you arrive. If necessary," +
                       " short naps will assist you in recovering without affecting your new rhythm."
                   },
                                        new BlogDetailsResponse
                   {
                       BlogId = "Blog5",
                       Name = "Charge Your Devices",
                       ImageUrl = "../images/blog/blog5/b5-5.jpg",
                       Description = "A dead phone battery can be a hassle, especially in unfamiliar places. Carry a portable charger " +
                       "and a universal travel adapter to stay connected. Some airports and hotels offer free charging stations, " +
                       "so take advantage of them when needed."
                   },
                                          new BlogDetailsResponse
                   {
                       BlogId = "Blog5",
                       Name = "Use Offline Maps and Translation Apps",
                       ImageUrl = "../images/blog/blog5/b5-6.jpg",
                       Description = "Don't get lost by downloading Google Maps offline in your destination. Language may be a problem, " +
                       "but use apps such as Google Translate, which includes offline translation and voice recognition that you" +
                       " can use to speak with locals."
                   },
                                           new BlogDetailsResponse
                   {
                       BlogId = "Blog5",
                       Name = "Protect Your Possessions",
                       ImageUrl = "../images/blog/blog5/b5-7.jpg",
                       Description = "Pickpocketing is possible in crowded tourist areas. Use anti-theft backpacks, store valuables in " +
                       "a money belt, and be wise in new places. Always store important documents and spare cash in hotel safes."
                   },
                                           new BlogDetailsResponse
                   {
                       BlogId = "Blog5",
                       Name = "Book Lodgings Strategically",
                       ImageUrl = "../images/blog/blog5/b5-8.jpg",
                       Description = "Branch out from conventional hotels and investigate Airbnb, hostels, or boutique hotels for unusual " +
                       "adventures. Careful reviews and checking addresses on maps can guarantee safe and comfortable lodging in your budget."
                   },
                                            new BlogDetailsResponse
                   {
                       BlogId = "Blog5",
                       Name = "Be Healthy During Travel",
                       ImageUrl = "../images/blog/blog5/b5-9.jpg",
                       Description = "Long flights and exposure to new surroundings can be tough on your health. Drink loads of water, carry" +
                       " essential medicines, and eat local cuisine carefully to prevent stomach problems. Keeping hand sanitizer and disinfectant" +
                       " wipes handy will keep you germ-free."
                   },
                                             new BlogDetailsResponse
                   {
                       BlogId = "Blog5",
                       Name = "Make the Most of Your Travel Experience",
                       ImageUrl = "../images/blog/blog5/b5-10.jpg",
                       Description = "Interact with the people, eat local food, and break free from your comfort bubble. Being flexible and willing " +
                       "to try new things will make your travels more worth it. Take pictures but also don't forget to live them."
                   },
                                             new BlogDetailsResponse
                   {
                       BlogId = "Blog6",
                       Name = "Participate in Local Festivals",
                       ImageUrl = "../images/blog/blog6/b6-1.jpg",
                       Description = "One of the best ways to experience a culture is by joining in its celebrations. Whether it’s the vibrant colors of " +
                       "India’s Holi festival, the electrifying energy of Brazil’s Carnival, or the lantern-lit skies of Thailand’s Yi Peng festival, local " +
                       "festivals provide a firsthand experience of a region’s history, values, and traditions."
                   },
                                              new BlogDetailsResponse
                   {
                       BlogId = "Blog6",
                       Name = "Savor Authentic Cuisine",
                       ImageUrl = "../images/blog/blog6/b6-2.jpg",
                       Description = "Food is a door to a culture. Rather than eating at international chain restaurants, eat at local restaurants, street stalls," +
                       " or even a home dinner with a local family. Discover the ingredients, preparation methods, and the history behind the traditional fare such " +
                       "as Japan's sushi, Italy's homemade pasta, or Morocco's scented tagine."
                   },
                                               new BlogDetailsResponse
                   {
                       BlogId = "Blog6",
                       Name = "Learn the Language and Customs",
                       ImageUrl = "../images/blog/blog6/b6-3.jpg",
                       Description = "A couple of words of local language will get long way in making friends with locals. Learn simple greetings, everyday expressions," +
                       " and local gestures. Familiarity with customs, like bowing in Japan, taking off shoes while entering a home in most Asian cultures, or eating with " +
                       "the right hand in Middle Eastern countries, proves to be respectful and adds depth to your experience."
                   },
                                               new BlogDetailsResponse
                   {
                       BlogId = "Blog6",
                       Name = "Explore Traditional Art and Craft",
                       ImageUrl = "../images/blog/blog6/b6-4.jpg",
                       Description = "Handicrafts and art are frequently imbued with profound cultural meaning. Stop by local markets, artisanal workshops, and cultural " +
                       "centers to observe craftsmen at work making textiles, pottery, paintings, and jewelry. From the elaborate Balinese wood carvings, Turkish ceramics, " +
                       "or Mexican Alebrijes, these artworks provide a window into a people's heritage and artistic traditions."
                   },
                                               new BlogDetailsResponse
                   {
                       BlogId = "Blog6",
                       Name = "Stay with Locals",
                       ImageUrl = "../images/blog/blog6/b6-5.jpg",
                       Description = "Choose homestays or locally-owned accommodations over commercial hotels. Living with a host family or in a traditional guesthouse " +
                       "enables you to live daily life, eat meals, and participate in cultural exchanges that would not be available in a standard hotel environment."
                   },
                                               new BlogDetailsResponse
                   {
                       BlogId = "Blog6",
                       Name = "Participate in Traditional Activities",
                       ImageUrl = "../images/blog/blog6/b6-6.jpg",
                       Description = "Get involved in experiential activities that are deeply valued by locals. Get cooking classes in Thai, flamenco dance lessons in " +
                       "Spain, or tea ceremonies in China. All these offer a deeper insight into the ways of culture and permit direct interaction with the local people."
                   },
                                               new BlogDetailsResponse
                   {
                       BlogId = "Blog6",
                       Name = "Honor and Maintain Cultural Heritage",
                       ImageUrl = "../images/blog/blog6/b6-7.jpg",
                       Description = "While enjoying a foreign culture, being a responsible traveler is crucial. Respects places of worship, permission to take photos, and " +
                       "obeys the locals' way of life. Empowering sustainable tourism practices, such as fair trade shopping and green tours, protects cultural heritage for" +
                       " generations to come."
                   },
                                               new BlogDetailsResponse
                   {
                       BlogId = "Blog6",
                       Name = "Final Thoughts",
                       ImageUrl = "../images/blog/blog6/b6-8.jpg",
                       Description = "Sinking into a culture's history and magic makes your travel experience richer but also your view of the world. By accepting customs, engaging" +
                       " in traditions, and honoring cultural heritage, you can turn an ordinary trip into a truly genuine journey with unforgettable experiences."
                   },
               };
            return blogDetailsResponses;
        }

        private List<BlogDetails> GetBlogDetails()
        {
            List<BlogDetails> BlogDetails = new List<BlogDetails>
        {
            new BlogDetails
            {
                BlogId = "Blog1",
                Title= "Unveiling Secret Escapes: Explore Lesser-Known Destinations",
                VideoURL = "https://www.youtube.com/embed/UqmJrNxnaY8",
                MainDescription = @"In a world where travel hotspots are often crowded with tourists, 
                discovering hidden gems can transform your journey into a truly unique experience. If you’re looking 
                to escape the usual tourist traps and explore destinations that offer authenticity, adventure, and 
                tranquility, this blog is for you. Let’s uncover some of the world’s best-kept travel secrets 
                that promise unforgettable experiences.",
                blogDetails = new List<BlogDetailsResponse>()
            },
            new BlogDetails
            {
                BlogId = "Blog2",
                Title="Guide to Cultural Festivals, Food Tours, and Authentic Local Experiences",
                VideoURL = "https://www.youtube.com/embed/ljf8yxDQ1d0",
                MainDescription = @"Traveling around the globe isn't merely a matter of seeing new sites; it's about experiencing the rich cultures, vibrant celebrations,
                and mouth-watering delicacies that set every place apart. Whether you are a culture lover, a foodie, or an adventurous traveler, living through a nation's customs is sure 
                to make your travel experience unforgettable. This travel guide will walk you through some of the most amazing cultural celebrations, must-visit food tours, and how
                to experience local traditions.",
                blogDetails = new List<BlogDetailsResponse>()
            },
             new BlogDetails
            {
                BlogId = "Blog3",
                Title="Opulent Retreats: Exclusive Resorts and Unforgettable Luxury Experiences",
                VideoURL = "https://www.youtube.com/embed/2g-dh-UVqsQ",
                MainDescription = @"In an era where travel is not a mere escape, luxury resorts redefine indulgence, exclusivity, and comfort. Whether set in 
                tropical paradises, ridges of tranquil mountains, or cityscapes of breathtaking beauty, these places are an experience like no other. This is an overview of
                the world of luxurious retreats and why they must feature on your travel bucket list.",
                blogDetails = new List<BlogDetailsResponse>()
            },
                        new BlogDetails
            {
                BlogId = "Blog4",
                Title="Affordable Getaways: Explore the World on a Budget",
                VideoURL = "https://www.youtube.com/embed/mtCK_tHQ6U4",
                MainDescription = @"World travel doesn't need to be expensive. With thoughtful planning, low-cost destinations, and savvy travel tips, you can have 
                amazing experiences without overspending. Pristine beaches, vibrant cities, or idyllic countryside getaways – no matter what kind of traveler you are, there are 
                affordable alternatives for you. Here's how you can see the world without breaking the bank.",
                blogDetails = new List<BlogDetailsResponse>()
            },
                        new BlogDetails
            {
                BlogId = "Blog5",
                Title="Smart Travel Secrets: Essential Tips and Hacks for a Hassle-Free Journey",
                VideoURL = "https://www.youtube.com/embed/L93-XaIVII4",
                MainDescription = @"Travel must be a fun and stress-free experience, but things can go awry sometimes. Whether you're a seasoned traveler or taking your 
                first trip, it helps to have some smart travel hacks in your back pocket. Here are some tips to make your journey smooth and enjoyable.",
                blogDetails = new List<BlogDetailsResponse>()
            },
                        new BlogDetails
            {
                BlogId = "Blog6",
                Title="Authentic Journeys: Dive into Local Traditions and Cultural Wonders",
                VideoURL = "https://www.youtube.com/embed/JG2PLvyYORE",
                MainDescription = @"Travel is not merely going to new destinations; it's about getting involved in the core of various cultures, living through traditions,
                and forming deep connections with the locals who inhabit these destinations. Real journeys enable travelers to move beyond tourist destinations and interact with the world 
                in a manner that promotes understanding, appreciation, and self-enrichment. Here's how you can really immerse yourself in local traditions and cultural marvels while traveling.",
                blogDetails = new List<BlogDetailsResponse>()
            }
            };
            return BlogDetails;
        }
    }
}
