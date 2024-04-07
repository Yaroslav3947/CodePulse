export interface UpdateCosmetic {
    name: string;
    brand: string;
    price: number;
    description: string;
    featuredImageUrl: string;
    urlHandle: string;
    publishedDate: Date;
    categories: string[];
}