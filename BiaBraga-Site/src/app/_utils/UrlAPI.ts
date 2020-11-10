export class UrlAPI {
    private static readonly UrlBase = 'http://localhost:5000/';
    public static readonly UrlUser = UrlAPI.UrlBase + 'api/user/';


    private static readonly UrlApiResources = UrlAPI.UrlBase + 'resources/';
    public static readonly UrlImagesCarousel = UrlAPI.UrlApiResources + 'images/carousel/';
}
