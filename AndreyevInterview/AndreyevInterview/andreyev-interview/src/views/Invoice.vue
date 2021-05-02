<template>
  <div class="about">
    <router-link :to="{ name: 'Invoices' }">
      <el-button>Back</el-button>
      </router-link>

    <h2>Invoice Details</h2>

    <span>Invoice #{{ $route.params.id }}</span>

    <h3>Line Items</h3>

    <table>
      <thead>
        <th>ID</th>
        <th>Description</th>
        <th>Quantity</th>
        <th>Cost</th>
        <th>Bill</th>
        <th>Total</th>
      </thead>
      <tbody>
        <tr v-for="item in state.lineItems" :key="item.id">
          <td>{{ item.id }}</td>
          <td>{{ item.description }}</td>
          <td>{{ item.quantity }}</td>
          <td>{{ item.cost }}</td>

          <td v-if ="item.bill === 1"> Billable</td>
          <td v-if ="item.bill === 2"> Non-billable</td>
         
          
          <td>{{ item.quantity * item.cost }}</td>
          <td>
              <link>      
                <el-button type="danger" v-on:click="deleteLineItem(item.id)">Delete</el-button>
          </td>
        </tr>
      </tbody>
    </table>

    <form @submit.prevent>
      <h4>Create Line Item</h4>

      Description: <input type="text" name="description" placeholder="Description" required v-model="state.description"/>
      Quantity: <input type="number" name="quantity" placeholder="Quantity" required v-model="state.quantity"/>
      Cost: <input type="number" name="cost" placeholder="Cost" required v-model="state.cost"/>

      Choose Bill option for this Item:
      <el-select placeholder="Choose Bill Option"  v-model="state.bill" >
        <el-option label = "Billable" :value = 1></el-option>
        <el-option label = "Non-billable" :value = 2></el-option>
      </el-select>

      <!-- <p v-if="state.bill === 1">You have choose: Billable</p>
      <p v-if="state.bill === 2">You have choose: Non-billable</p>
      <p>{{state.bill}}</p> -->


      <br /><br />
          <el-button @click="createLineItem">Add New Item</el-button>

      <el-row :gutter="20">
        <el-col :span="6"><div class="grid-content bg-purple"></div></el-col>
        <el-col :span="6"><div class="grid-content bg-purple"></div></el-col>
        <el-col :span="6"><div class="grid-content bg-purple"></div></el-col>
        <el-col :span="6"
          ><div class="grid-content bg-purple"></div>
          <h4>Subtotal</h4>
          <h5>Total Value:</h5>

          <div> {{totalvalue}} </div>
          <h5>Total Billable Value :</h5>
          <span>{{totalbillablevalue}}</span>
          <h5>Total Number Items:</h5>
          <span>{{totalnumberitems}}</span>
          
        </el-col>
      </el-row>
    </form>
  </div>
</template>

<script lang="ts">
import { computed, defineComponent, onMounted, reactive} from "vue";

export default defineComponent({

  name: "Invoice",

  props: {
    id: {
      type: [String, Number],
      required: true,
    },
  },
  setup(props) {
    const state = reactive({
      lineItems: [],
      description: "",
      quantity: "0",
      cost: "0",
      total: "0",
      bill:"",
    });

    const totalvalue = computed(()=>{
      let result =0;
      for (let i=0;i<state.lineItems.length;i++){
        result = result + state.lineItems[i]['quantity'] * state.lineItems[i]['cost'];
      }
      return result;
    })

    const totalbillablevalue = computed(()=>{
      let result =0;
      for (let i=0;i<state.lineItems.length;i++){
        if(state.lineItems[i]['bill'] === 1)
        {
          result = result + state.lineItems[i]['quantity'] * state.lineItems[i]['cost'];
        } 
      }
      return result;  

    })

    const totalnumberitems = computed(()=>{
      let result =0;
      for (let i=0;i<state.lineItems.length;i++){
        result = result +1;
      }
      return result;

    })

    function fetchLineItems() {
      fetch(`http://localhost:5000/invoices/${props.id}`, {
        method: "GET",
        headers: {
          "Content-Type": "application/json",
        },
      }).then((response) => {
        response.json().then((lineItems) => (state.lineItems = lineItems));
      });
    }
    function createLineItem() {
      fetch(`http://localhost:5000/invoices/${props.id}`, {
        method: "POST",
        headers: {
          "Content-Type": "application/json",
        },
        body: JSON.stringify({
          description: state.description,
          quantity: Number(state.quantity),
          cost: Number(state.cost),
          bill: Number(state.bill),
        }),
      }).then(fetchLineItems);
      updateTotalInfo();
    }

    function deleteLineItem(e:Number)
    {

        // console.log(e);

        fetch(`http://localhost:5000/invoices/${e}`, {
        method: "DELETE",
        headers: {
          "Content-Type": "application/json",
        },
        body: JSON.stringify({
          id:e
        }),
      }).then(fetchLineItems);
      updateTotalInfo();
    }

    function updateTotalInfo(){


      console.log(totalvalue.value);
      
      fetch(`http://localhost:5000/invoices/${props.id}`, {
        method: "PUT",
        headers: {
          "Content-Type": "application/json",
        },
        body: JSON.stringify({
          totalValue: Number(totalvalue.value),
          totalBillableValue: Number(totalbillablevalue.value),
          totalNumberLineItems: Number(totalnumberitems.value),
        }),
      });
 
    }
    onMounted(fetchLineItems);
    return { state, createLineItem, deleteLineItem, totalvalue,totalbillablevalue,totalnumberitems};
  },

  
  methods:{
  },
  computed:{

  },
});
</script>