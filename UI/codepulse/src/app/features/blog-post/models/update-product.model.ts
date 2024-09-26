export interface UpdateProduct {
    name: string;
    stock: number;
    price: number;
    description: string;
    featuredImageUrl: string;
    urlHandle: string;
    publishedDate: Date;
    categories: string[];
}