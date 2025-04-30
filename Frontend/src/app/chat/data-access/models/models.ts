export interface AdvertPublicStatus {
    label: string;
    name: string;
  }
  
  export interface AdvertPrice {
    usd: number;
    byn: number;
    rub: number;
    eur: number;
  }
  
  export interface AdvertPreviewResponse {
    id: number;
    advertType: string;
    publicStatus: AdvertPublicStatus;
    advertStatus: string;
    sellerName: string;
    price: AdvertPrice;
    photoPreviewUrl: string;
    brand: string;
    model: string;
    generation: string;
    year: number;
  }
  
  export interface GetUserDialogsResponse {
    advertInfo: AdvertPreviewResponse;
    name: string;
    dialogId: number;
    isAdvertOwner: boolean;
    lastMessageTime: string;
    lastMessage: string;
  }

  export interface CheckDialogResponse {
    advertInfo: AdvertPreviewResponse;
    id: number | null;
    name: string | null;
  }

  