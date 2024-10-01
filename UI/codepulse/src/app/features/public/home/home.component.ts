import { Component, OnInit } from '@angular/core';
import { ProductService } from '../../blog-post/services/product.service';
import { Observable } from 'rxjs';
import { Product } from '../../blog-post/models/product.model';
import { CategoryService } from '../../category/services/category.service';
import { Category } from '../../category/models/category.model';
import { OrderServiceService } from '../../blog-post/services/order.service.service';
import { AuthService } from '../../auth/services/auth.service';
import { UserModel } from '../../auth/models/user.model';
import { Order } from '../../blog-post/models/order.model';
import { CreateOrderRequest } from '../../blog-post/models/create-order-request';
import { OrderItem } from '../../blog-post/models/order-item';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {
  products$?: Observable<Product[]>;
  filteredProducts: Product[] = [];
  products: Product[] = [];
  searchStock: string = '';
  categories: Category[] = [];
  selectedCategories: Category[] = [];
  orderId: string | null = null;
  user?: UserModel;
  orderItem: OrderItem | undefined;
  orderItems: OrderItem[] = [];
  constructor(
    private productService: ProductService,
    private categoryService: CategoryService,
    private orderService: OrderServiceService,
    private authService: AuthService
  ) {}

  ngOnInit(): void {
    this.user = this.authService.getUser();
    this.products$ = this.productService.getAllProducts();
    this.productService.getAllProducts().subscribe(products => {
      this.products = products;
      this.filteredProducts = products;
    });

    this.categoryService.getAllCategories().subscribe({
      next: (response) => {
        this.categories = response.map(category => ({ ...category, checked: true }));
        this.selectedCategories = [...this.categories];
        this.filterproducts();
      }
    });

    this.initializeOrder();
  }

  initializeOrder(): void {
    this.orderService.getOrderByUserId(this.user!.userId).subscribe({
      next: (order) => {
        if (order) {
          this.orderId = order.id;
        } else {
          this.createOrder();
          console.error('Created Order');
        }
      },
      error: (err) => {
        console.error('Error fetching order:', err);
      }
    });
  }

  createOrder(): void {
    const createOrderRequest: CreateOrderRequest = {
      userId: this.user!.userId
    };

    this.orderService.createOrder(createOrderRequest).subscribe({
      next: (response: Order) => {
        this.orderId = response.id;
        console.log('New order created:', response);
      },
      error: (err) => {
        console.error('Error creating order:', err);
      }
    });
  }

  // increaseOrderItemQuantity(product: Product): void {
  //   const existingOrderItem: OrderItem | undefined = this.findOrderItemByProductId(product.id);
  
  //   if (existingOrderItem) {
  //     if (existingOrderItem.quantity < product.stock) {
  //       existingOrderItem.quantity += 1; // Increase quantity
  //       existingOrderItem.totalCost = existingOrderItem.price * existingOrderItem.quantity; 
  //       this.updateProductQuantityInOrder(existingOrderItem); // Call update API
  //     }
  //   } else {
  //     const newOrderItem: OrderItem = {
  //       productId: product.id,
  //       product: product,
  //       quantity: 1,
  //       price: product.price,
  //       totalCost: product.price 
  //     };
      
  //     // Add to order and call API
  //     this.addToOrder(newOrderItem.product);
  //   }
  // }
  
  // decreaseOrderItemQuantity(product: Product): void {
  //   const orderItem = this.orderItems.find(item => item.productId === product.id);
  
  //   if (orderItem && orderItem.quantity > 1) {
  //     orderItem.quantity -= 1; 
  //     orderItem.totalCost = orderItem.price * orderItem.quantity; 
  
  //     this.updateProductQuantityInOrder(orderItem);
  //   }
  // }
  
  // findOrderItemByProductId(productId: string): OrderItem | undefined {
  //   return this.orderItems.find(item => item.productId === productId); // Assuming orderItems is an array of OrderItem
  // }

  // addToOrder(product: Product): void {
  //     if (this.user && this.orderId) {
  //         const orderItem: OrderItem = {
  //             productId: product.id,
  //             product: product, 
  //             quantity: 1, 
  //             price: product.price,
  //             totalCost: product.price
  //         };
  //             this.orderService.addProductToOrder(this.orderId!, product.id, orderItem.quantity).subscribe({
  //                 next: (response: Order) => {
  //                     console.log('Product added to order:', response);
  //                 },
  //                 error: (err) => {
  //                     console.error('Error adding product to order:', err);
  //                 }
  //             });
  //         }
  // }


  // // Update the quantity of a specific product in the order
  // updateProductQuantityInOrder(orderItem: OrderItem): void {
  //     if (this.user && this.orderId) {
  //         this.orderService.updateProductQuantityInOrder(this.orderId, orderItem.productId, orderItem.quantity).subscribe({
  //             next: (response: Order) => {
  //                 console.log('Product quantity updated in order:', response);
  //             },
  //             error: (err) => {
  //                 console.error('Error updating product quantity in order:', err);
  //             }
  //         });
  //     }
  // }

  filterByStock() {
    console.log(this.filteredProducts.length);
    const searchStockNumber = parseInt(this.searchStock.trim(), 10); 
    
    if (isNaN(searchStockNumber)) {
      this.filteredProducts = this.products;
    } else {
      this.filteredProducts = this.products.filter(product =>
        product.stock >= searchStockNumber 
      );
    }
  }


  filterproducts(): void {
    if (this.selectedCategories.length === 0) {
      this.filteredProducts = this.products;
    } else {
      this.filteredProducts = this.products.filter(product =>
        this.selectedCategories.some(category => product.categories.some(c => c.id === category.id))
      ); 
    }
  }

  onCheckboxChange(category: Category): void {
    const index = this.selectedCategories.findIndex(c => c.id === category.id);
    if (index !== -1) {
      this.selectedCategories.splice(index, 1); 
    } else {
      this.selectedCategories.push(category); 
    }
    this.filterproducts(); 
  }
  

}


