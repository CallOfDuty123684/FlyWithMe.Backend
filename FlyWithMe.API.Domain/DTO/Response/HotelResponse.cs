namespace FlyWithMe.API.Domain.DTO.Response
{
    using System;
    using System.Collections.Generic;
    using System.Text.Json.Serialization;

    public class HotelOffersResponse : ReturnResponse
    {
        [JsonPropertyName("data")]
        public List<HotelOfferData> Data { get; set; }

        [JsonPropertyName("warnings")]
        public List<ApiWarning> Warnings { get; set; }
    }

    public class HotelOfferData
    {
        [JsonPropertyName("type")]
        public string Type { get; set; }

        [JsonPropertyName("hotel")]
        public HotelInfo Hotel { get; set; }

        [JsonPropertyName("available")]
        public bool Available { get; set; }

        [JsonPropertyName("offers")]
        public List<HotelOffer> Offers { get; set; }

        [JsonPropertyName("self")]
        public string Self { get; set; }
    }

    public class HotelInfo
    {
        [JsonPropertyName("type")]
        public string Type { get; set; }

        [JsonPropertyName("hotelId")]
        public string HotelId { get; set; }

        [JsonPropertyName("chainCode")]
        public string ChainCode { get; set; }

        [JsonPropertyName("dupeId")]
        public string DupeId { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("cityCode")]
        public string CityCode { get; set; }

        [JsonPropertyName("latitude")]
        public double Latitude { get; set; }

        [JsonPropertyName("longitude")]
        public double Longitude { get; set; }
    }

    public class HotelOffer
    {
        [JsonPropertyName("id")]
        public string Id { get; set; }

        [JsonPropertyName("checkInDate")]
        public string CheckInDate { get; set; }

        [JsonPropertyName("checkOutDate")]
        public string CheckOutDate { get; set; }

        [JsonPropertyName("rateCode")]
        public string RateCode { get; set; }

        [JsonPropertyName("rateFamilyEstimated")]
        public RateFamily RateFamilyEstimated { get; set; }

        [JsonPropertyName("room")]
        public RoomDetails Room { get; set; }

        [JsonPropertyName("guests")]
        public GuestDetails Guests { get; set; }

        [JsonPropertyName("price")]
        public PriceDetails Price { get; set; }

        [JsonPropertyName("policies")]
        public PolicyDetails Policies { get; set; }

        [JsonPropertyName("self")]
        public string Self { get; set; }
    }

    public class RateFamily
    {
        [JsonPropertyName("code")]
        public string Code { get; set; }

        [JsonPropertyName("type")]
        public string Type { get; set; }
    }

    public class RoomDetails
    {
        [JsonPropertyName("type")]
        public string Type { get; set; }

        [JsonPropertyName("typeEstimated")]
        public RoomTypeEstimated TypeEstimated { get; set; }

        [JsonPropertyName("description")]
        public RoomDescription Description { get; set; }
    }

    public class RoomTypeEstimated
    {
        [JsonPropertyName("beds")]
        public int Beds { get; set; }

        [JsonPropertyName("bedType")]
        public string BedType { get; set; }
    }

    public class RoomDescription
    {
        [JsonPropertyName("text")]
        public string Text { get; set; }

        [JsonPropertyName("lang")]
        public string Lang { get; set; }
    }

    public class GuestDetails
    {
        [JsonPropertyName("adults")]
        public int Adults { get; set; }
    }

    public class PriceDetails
    {
        [JsonPropertyName("currency")]
        public string Currency { get; set; }

        [JsonPropertyName("base")]
        public string Base { get; set; }

        [JsonPropertyName("total")]
        public string Total { get; set; }

        [JsonPropertyName("variations")]
        public PriceVariations Variations { get; set; }
    }

    public class PriceVariations
    {
        [JsonPropertyName("average")]
        public PriceAverage Average { get; set; }

        [JsonPropertyName("changes")]
        public List<PriceChange> Changes { get; set; }
    }

    public class PriceAverage
    {
        [JsonPropertyName("base")]
        public string Base { get; set; }
    }

    public class PriceChange
    {
        [JsonPropertyName("startDate")]
        public string StartDate { get; set; }

        [JsonPropertyName("endDate")]
        public string EndDate { get; set; }

        [JsonPropertyName("base")]
        public string Base { get; set; }
    }

    public class PolicyDetails
    {
        [JsonPropertyName("cancellations")]
        public List<CancellationPolicy> Cancellations { get; set; }

        [JsonPropertyName("paymentType")]
        public string PaymentType { get; set; }

        [JsonPropertyName("refundable")]
        public RefundablePolicy Refundable { get; set; }
    }

    public class CancellationPolicy
    {
        [JsonPropertyName("numberOfNights")]
        public int NumberOfNights { get; set; }

        [JsonPropertyName("deadline")]
        public string Deadline { get; set; }

        [JsonPropertyName("amount")]
        public string Amount { get; set; }

        [JsonPropertyName("policyType")]
        public string PolicyType { get; set; }
    }

    public class RefundablePolicy
    {
        [JsonPropertyName("cancellationRefund")]
        public string CancellationRefund { get; set; }
    }

    public class ApiWarning
    {
        [JsonPropertyName("code")]
        public int Code { get; set; }

        [JsonPropertyName("title")]
        public string Title { get; set; }

        [JsonPropertyName("detail")]
        public string Detail { get; set; }

        [JsonPropertyName("source")]
        public WarningSource Source { get; set; }
    }

    public class WarningSource
    {
        [JsonPropertyName("parameter")]
        public string Parameter { get; set; }
    }


}
