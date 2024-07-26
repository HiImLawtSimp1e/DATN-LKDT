import Loading from "@/components/shop/loading";
import { Suspense } from "react";
import HomeShopProductList from "@/components/shop/home/home-product-list";
import Slider from "@/components/shop/home/home-slider";
import { slides } from "@/lib/mock/slide";

const Slides = () => {
  return <Slider slides={slides} />;
};

const Products = async () => {
  const res = await fetch(`http://localhost:5000/api/Product`, {
    method: "GET",
  });

  const responseData: ApiResponse<PagingParams<IProduct[]>> = await res.json();
  const { data, success, message } = responseData;
  // console.log(responseData);
  const { result, pages, currentPage } = data;

  return <HomeShopProductList products={result} />;
};

const Home = () => {
  return (
    <div className="bg-gray-100 pb-24">
      <Suspense fallback={<Loading />}>
        <Slides />
      </Suspense>
      <div className="mt-24 px-4 md:px-8 lg:px-16 xl:px-32 2xl:px-64">
        <h1 className="text-2xl">Sản phẩm mới</h1>
        <Suspense fallback={<Loading />}>
          <Products />
        </Suspense>
      </div>
    </div>
  );
};

export default Home;
