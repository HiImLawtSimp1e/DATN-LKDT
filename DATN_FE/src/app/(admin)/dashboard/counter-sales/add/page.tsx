import CounterSales from "@/components/dashboard/counter-sales/counter-sales";
import AdminLoading from "@/components/dashboard/loading";
import { Suspense } from "react";

const CounterSalePage = () => {
  return (
    <Suspense fallback={<AdminLoading />}>
      <CounterSales />
    </Suspense>
  );
};

export default CounterSalePage;
